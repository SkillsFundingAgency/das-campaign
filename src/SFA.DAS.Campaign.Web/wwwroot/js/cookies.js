
;(function () {
  'use strict'

  // ---------------------------------------------------------------------------
  // Cookie helpers
  // ---------------------------------------------------------------------------

  function getCookie(name, defaultValue) {
    var prefix = name + '='
    var match = document.cookie
      .split(';')
      .map(function (cookie) {
        return cookie.trim()
      })
      .find(function (cookie) {
        return cookie.indexOf(prefix) === 0
      })

    return match ? decodeURIComponent(match.slice(prefix.length)) : defaultValue || null
  }

  function setCookie(name, value, options) {
    options = options || {}
    var cookie = name + '=' + value + '; path=/'

    if (options.days) {
      // max-age is in seconds
      cookie += '; max-age=' + options.days * 24 * 60 * 60
    }
    if (document.location.protocol === 'https:') {
      cookie += '; Secure'
    }

    document.cookie = cookie
  }

  // ---------------------------------------------------------------------------
  // Consent
  // ---------------------------------------------------------------------------

  var POLICY_COOKIE = 'cookies_policy'
  var PREFERENCES_SET_COOKIE = 'cookies_preferences_set'
  var COOKIE_LIFETIME_DAYS = 365

  var Consent = {
    ACCEPT_ALL: { essential: true, campaigns: true, settings: true, usage: true },
    REJECT_ALL: { essential: true, campaigns: false, settings: false, usage: false },

    listeners: [],

    // The current policy, or {} when the visitor has not chosen yet.
    getPolicy: function () {
      try {
        return JSON.parse(getCookie(POLICY_COOKIE, '{}')) || {}
      } catch (error) {
        return {}
      }
    },

    preferencesSet: function () {
      return getCookie(PREFERENCES_SET_COOKIE) === 'true'
    },

    hasAcceptedAdditionalCookies: function (policy) {
      return Object.keys(policy).some(function (category) {
        return category !== 'essential' && policy[category]
      })
    },


    savePolicy: function (policy, markPreferencesSet) {
      setCookie(POLICY_COOKIE, JSON.stringify(policy), { days: COOKIE_LIFETIME_DAYS })
      if (markPreferencesSet) {
        setCookie(PREFERENCES_SET_COOKIE, 'true', { days: COOKIE_LIFETIME_DAYS })
      }
    },

    onStatusLoaded: function (callback) {
      this.listeners.push(callback)
    },

    // Save an actively-chosen policy and notify every subscriber.
    setStatus: function (policy) {
      this.savePolicy(policy, true)
      this.listeners.forEach(function (callback) {
        callback(policy)
      })
    },
  }

  // ---------------------------------------------------------------------------
  // Cookie banner
  // ---------------------------------------------------------------------------

  function CookieBanner($module) {
    this.$module = $module
  }

  CookieBanner.prototype.init = function () {
    this.message = this.$module.querySelector('.js-cookie-banner-message')
    this.confirmAccept = this.$module.querySelector('.js-cookie-banner-confirmation-accept')
    this.confirmReject = this.$module.querySelector('.js-cookie-banner-confirmation-reject')

    this.$module
      .querySelector('[data-accept-cookies]')
      .addEventListener('click', this.accept.bind(this))
    this.$module
      .querySelector('[data-reject-cookies]')
      .addEventListener('click', this.reject.bind(this))
    this.$module.querySelectorAll('[data-hide-cookie-message]').forEach(
      function (node) {
        node.addEventListener('click', this.hide.bind(this))
      }.bind(this)
    )

    // Hide the banner if the policy is saved elsewhere (e.g. the cookies page).
    Consent.onStatusLoaded(this.hide.bind(this))

    this.show()
  }

  CookieBanner.prototype.show = function () {
    if (Consent.preferencesSet()) {
      this.hide()
      return
    }

    var policy = Consent.getPolicy()
    var noResponse = Object.keys(policy).length === 0
    var acceptedAdditional = Consent.hasAcceptedAdditionalCookies(policy)

    this.$module.hidden = false
    this.confirmAccept.hidden = noResponse || !acceptedAdditional
    this.confirmReject.hidden = noResponse || acceptedAdditional
  }

  CookieBanner.prototype.hide = function () {
    this.$module.hidden = true
  }

  CookieBanner.prototype.accept = function () {
    Consent.savePolicy(Consent.ACCEPT_ALL, true)
    this.showConfirmation(this.confirmAccept)
  }

  CookieBanner.prototype.reject = function () {
    Consent.savePolicy(Consent.REJECT_ALL, true)
    this.showConfirmation(this.confirmReject)
  }

  CookieBanner.prototype.showConfirmation = function (confirmation) {
    this.message.hidden = true
    confirmation.hidden = false
    confirmation.focus()
  }

  // ---------------------------------------------------------------------------
  // Cookie settings form (/cookies)
  // ---------------------------------------------------------------------------

  function CookieSettings($module) {
    this.$module = $module
  }

  CookieSettings.prototype.init = function () {
    this.$module.addEventListener('submit', this.submit.bind(this))

    var policy = Consent.getPolicy()
    if (Object.keys(policy).length === 0) {
      // Default to rejecting additional cookies, but without marking
      // preferences as set so the banner still prompts for a choice.
      policy = Consent.REJECT_ALL
      Consent.savePolicy(policy, false)
    }
    this.setFormValues(policy)

    // Keep the form in sync if the policy changes elsewhere.
    Consent.onStatusLoaded(this.setFormValues.bind(this))
  }

  CookieSettings.prototype.setFormValues = function (policy) {
    Object.keys(policy).forEach(
      function (category) {
        if (category === 'essential') {
          // Essential cookies cannot be turned off by the user.
          return
        }
        var input = this.$module.querySelector(
          'input[name=cookies-' + category + '][value=' + (policy[category] ? 'on' : 'off') + ']'
        )
        if (input) {
          input.checked = true
        }
      }.bind(this)
    )
  }

  CookieSettings.prototype.getFormValues = function () {
    var values = { essential: true }
    this.$module.querySelectorAll('input').forEach(function (input) {
      if (input.checked) {
        values[input.name.replace('cookies-', '')] = input.value === 'on'
      }
    })
    return values
  }

  CookieSettings.prototype.submit = function (event) {
    event.preventDefault()
    Consent.setStatus(this.getFormValues())
    this.showConfirmationMessage()
  }

  CookieSettings.prototype.showConfirmationMessage = function () {
    var confirmation = document.querySelector('[data-cookie-confirmation]')
    if (!confirmation) {
      return
    }
    // Toggle display so assistive tech re-announces the live region.
    confirmation.style.display = 'none'
    window.scrollTo(0, 0)
    confirmation.style.display = 'block'
  }
  
  function isCrossOrigin(href) {
    try {
      return new URL(href, location.href).origin !== location.origin
    } catch (error) {
      return false
    }
  }

  function initUidSharing() {
    var uidKey = 'consent_uid'
    var url = new URL(location.href)
    var uid = getCookie(uidKey) || url.searchParams.get(uidKey)

    // Remove the uid parameter from the visible URL.
    if (url.searchParams.has(uidKey)) {
      url.searchParams.delete(uidKey)
      history.replaceState(null, '', url.toString())
    }

    if (!uid) {
      return
    }

    setCookie(uidKey, uid, { days: COOKIE_LIFETIME_DAYS })

    // Append the uid to cross-origin links when they are clicked.
    document.querySelectorAll('a[href]').forEach(function (link) {
      if (isCrossOrigin(link.href)) {
        link.addEventListener('click', function (event) {
          var target = event.currentTarget
          var linkUrl = new URL(target.href)
          linkUrl.searchParams.set(uidKey, uid)
          target.href = linkUrl.toString()
        })
      }
    })
  }

  // ---------------------------------------------------------------------------
  // Wiring
  // ---------------------------------------------------------------------------

  document.addEventListener('DOMContentLoaded', function () {
    initUidSharing()

    var banner = document.querySelector('[data-fiu-shared-cookie-consent]')
    if (banner) {
      new CookieBanner(banner).init()
    }

    document.querySelectorAll('[data-module~="cookie-settings-shared"]').forEach(function (form) {
      new CookieSettings(form).init()
    })
  })
})()

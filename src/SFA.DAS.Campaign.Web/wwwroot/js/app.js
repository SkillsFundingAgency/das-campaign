/* global Consent, Utils */

;(function () {

    var Utils = (window.Utils = {})

    Utils.acceptedAdditionalCookies = function (cookiesPolicy) {
      for (var category in cookiesPolicy) {
        if (
          Object.prototype.hasOwnProperty.call(cookiesPolicy, category) &&
          category !== 'essential' &&
          cookiesPolicy[category]
        ) {
          return true
        }
      }
      return false
    }
  
    Utils.isEmpty = function (obj) {
      for (var x in obj) {
        if (Object.prototype.hasOwnProperty.call(obj, x)) return false
      }
      return true
    }
  
    Utils.getCookie = function (name, defaultValue) {
      name += '='
      var cookies = document.cookie.split(';')
      for (var index = 0; cookies.length > index; index++) {
        if (cookies[index].trim().slice(0, name.length) === name) {
          return decodeURIComponent(cookies[index].trim().slice(name.length))
        }
      }
      return defaultValue || null
    }
  
    Utils.setCookie = function (name, value, options) {
      document.cookie = name
        .concat('=', value)
        .concat(
          '; path=/',
          options.days
            ? '; max-age='.concat(options.days * 24 * 60 * 60 * 1000)
            : '',
          document.location.protocol === 'https:' ? '; Secure' : ''
        )
    }


    function CookieBannerShared($module) {
      this.$module = $module
    }
  
    CookieBannerShared.prototype.init = function () {
      this.cookies_preferences_set =
        Utils.getCookie('cookies_preferences_set') === 'true'
      this.cookies_policy = JSON.parse(Utils.getCookie('cookies_policy', '{}'))
  
      this.$module.banner = this.$module.querySelector(
        '.js-cookie-banner'
      )
      this.$module.message = this.$module.querySelector(
        '.js-cookie-banner-message'
      )
      this.$module.confirmAccept = this.$module.querySelector(
        '.js-cookie-banner-confirmation-accept'
      )
      this.$module.confirmReject = this.$module.querySelector(
        '.js-cookie-banner-confirmation-reject'
      )
  
      this.$module.setCookieConsent = this.acceptCookies.bind(this)
      this.$module.showAcceptConfirmation = this.showAcceptConfirmation.bind(this)
      this.$module
        .querySelector('[data-accept-cookies]')
        .addEventListener('click', this.$module.setCookieConsent)
      this.$module.rejectCookieConsent = this.rejectCookies.bind(this)
      this.$module.showRejectConfirmation = this.showRejectConfirmation.bind(this)
      this.$module
        .querySelector('[data-reject-cookies]')
        .addEventListener('click', this.$module.rejectCookieConsent)
  
      var nodes = this.$module.querySelectorAll('[data-hide-cookie-message]')
      for (var i = 0, length = nodes.length; i < length; i++) {
        nodes[i].addEventListener('click', this.hideBanner.bind(this))
      }
  
      Consent.onStatusLoaded(
        function (status) {
          this.setCookiesPolicyCookie(status)
          this.hideBanner()
        }.bind(this)
      )
  
      this.showBanner()
    }
  
    CookieBannerShared.prototype.showBanner = function () {
      var noResponse = Utils.isEmpty(this.cookiesPolicy)
      var acceptedAdditionalCookies = Utils.acceptedAdditionalCookies(
        this.cookies_policy
      )
  
      if (this.cookies_preferences_set) {
        this.hideBanner()
      } else {
        this.$module.hidden = false
        this.$module.confirmAccept.hidden = noResponse || !acceptedAdditionalCookies
        this.$module.confirmReject.hidden = noResponse || acceptedAdditionalCookies
        Utils.setCookie('cookies_preferences_set', 'true', { days: 365 })
      }
    }
  
    CookieBannerShared.prototype.hideBanner = function () {
      this.$module.hidden = true
    }
  
    CookieBannerShared.prototype.acceptCookies = function () {
      this.$module.showAcceptConfirmation()
      this.setCookiesPolicyCookie(Consent.ACCEPT_ALL)
      Consent.setStatus(Consent.ACCEPT_ALL)
    }
  
    CookieBannerShared.prototype.setCookiesPolicyCookie = function (cookiesPolicy) {
      Utils.setCookie('cookies_policy', JSON.stringify(cookiesPolicy), {
        days: 365,
      })
      Utils.setCookie('cookies_preferences_set', 'true', { days: 365 })
    }
  
    CookieBannerShared.prototype.showAcceptConfirmation = function () {
      this.$module.message.hidden = true
      this.$module.confirmAccept.hidden = false
      this.$module.confirmAccept.focus()
    }
  
    CookieBannerShared.prototype.rejectCookies = function () {
      this.$module.showRejectConfirmation()
      this.setCookiesPolicyCookie(Consent.REJECT_ALL)
      Consent.setStatus(Consent.REJECT_ALL)
    }
  
    CookieBannerShared.prototype.showRejectConfirmation = function () {
      this.$module.message.hidden = true
      this.$module.confirmReject.hidden = false
      this.$module.confirmReject.focus()
    }
  
    window.CookieBannerShared = CookieBannerShared
  
    document.addEventListener('DOMContentLoaded', function () {
      const cookieBannerShared = document.querySelector('[data-fiu-shared-cookie-consent]');

      if (cookieBannerShared) {
        new CookieBannerShared(cookieBannerShared).init();
      }
    })

  })()
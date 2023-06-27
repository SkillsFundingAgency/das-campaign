/* global Consent, Utils */

;(function () {
    function CookieSettings($module) {
      this.$module = $module
    }
  
    CookieSettings.prototype.init = function () {

      this.$module.submitSettingsForm = this.submitSettingsForm.bind(this)
      this.$module.getFormValues = this.getFormValues.bind(this)
      this.$module.setFormValues = this.setFormValues.bind(this)
  
      document
        .querySelector('form[data-module=cookie-settings-shared]')
        .addEventListener('submit', this.$module.submitSettingsForm)
  
      this.cookiesPolicy = JSON.parse(Utils.getCookie('cookies_policy'))
      if (!this.cookiesPolicy) {
        this.cookiesPolicy = Consent.REJECT_ALL
        Utils.setCookie('cookies_policy', JSON.stringify(this.cookiesPolicy), {
          days: 365,
        })
      }
  
      this.setFormValues(this.cookiesPolicy)
  
      Consent.onStatusLoaded(this.setFormValues.bind(this))

    }
  
    CookieSettings.prototype.setFormValues = function (cookiesPolicy) {

      cookiesPolicy = cookiesPolicy || this.cookiesPolicy
  
      for (var category in cookiesPolicy) {
        if (category === 'essential') {
          // this cannot be set by the user
          continue
        }
        var input = this.$module.querySelector(
          'input'
            .concat('[name=cookies-', category, ']')
            .concat('[value=', cookiesPolicy[category] ? 'on' : 'off', ']')
        )
        if (input) {
          input.checked = true
        }
      }
    }
  
    CookieSettings.prototype.getFormValues = function (form) {
      form = form || this.$module
      var formInputs = form.getElementsByTagName('input')
      var values = {}
  
      for (var i = 0; i < formInputs.length; i++) {
        var input = formInputs[i]
        if (input.checked) {
          var name = input.name.replace('cookies-', '')
          var value = input.value === 'on'
  
          values[name] = value
        }
      }
  
      values.essential = true
  
      return values
    }
  
    CookieSettings.prototype.submitSettingsForm = function (event) {
      event.preventDefault()
  
      this.cookiesPolicy = this.getFormValues(event.target)
  
      Utils.setCookie('cookies_policy', JSON.stringify(this.cookiesPolicy), {
        days: 365,
      })
      Utils.setCookie('cookies_preferences_set', true, { days: 365 })
  
      Consent.setStatus(this.cookiesPolicy)
  
      this.showConfirmationMessage()
    }
  
    CookieSettings.prototype.showConfirmationMessage = function () {
      var confirmationMessage = document.querySelector(
        'div[data-cookie-confirmation]'
      )
      // hide the message if already visible so assistive tech is triggered when it appears
      confirmationMessage.style.display = 'none'
      var previousPageLink = document.querySelector('.cookie-settings__prev-page')
      var referrer = CookieSettings.prototype.getReferrerLink()
  
      document.body.scrollTop = document.documentElement.scrollTop = 0
  
      confirmationMessage.style.display = 'block'
    }
  
    CookieSettings.prototype.getReferrerLink = function () {
      return document.referrer ? new URL(document.referrer).pathname : false
    }
  
    document.addEventListener('DOMContentLoaded', function () {
      var nodes = document.querySelectorAll('[data-module~="cookie-settings-shared"]')
      for (var index = 0; nodes.length > index; index++) {
        new CookieSettings(nodes[index]).init()
      }
    })
  })()
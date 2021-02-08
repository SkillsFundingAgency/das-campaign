
window.onload = function(event) {
    var errorSummary = document.querySelector('.govuk-error-summary');
    if (errorSummary !== null) {
        var pageTitle = document.querySelector('h1.govuk-heading-xl').innerText;
        var validationErrors = errorSummary.querySelectorAll('ul.govuk-error-summary__list li');
        var numberOfErrors = validationErrors.length;
        var validationMessage = 'Form validation';
        var dataLayerObj;
        if (numberOfErrors === 1) {
            validationMessage = validationErrors[0].innerText
        }
        dataLayerObj = {
            event: 'form submission error',
            page: pageTitle,
            message: validationMessage
        }
        window.dataLayer.push(dataLayerObj)
    }
};
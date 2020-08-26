using System;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css
{
    public class DefaultErrorCssViewModel : IErrorCssViewModel
    {
        public DefaultErrorCssViewModel()
        {

        }
        public DefaultErrorCssViewModel(string classPrefix)
        {
            _classPrefix = classPrefix;
        }
        public DefaultErrorCssViewModel(string classPrefix, string classModifier)
        {
            _classPrefix = classPrefix;
            _classModifier = classModifier;
        }

        private readonly string _classModifier = string.Empty;
        private readonly string _classPrefix = "govuk-";

        public string Summary => $"{_classPrefix}error-summary";
        public string SummaryTitle => $"{_classPrefix}error-summary__title";
        public string SummaryBody => $"{_classPrefix}error-summary__body";
        public string SummaryList => $"{_classPrefix}error-summary__list";
    }
}

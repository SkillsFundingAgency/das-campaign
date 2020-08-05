using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css
{
    public class DefaultUtilitiesCssViewModel : IUtilitiesCssViewModel
    {
        public DefaultUtilitiesCssViewModel()
        {

        }
        public DefaultUtilitiesCssViewModel(string classPrefix)
        {
            ClassPrefix = classPrefix;
        }

        public string ClassPrefix { get; set; } = "govuk-";
        public string FontWeightBold => $"{ClassPrefix}!-font-weight-bold";
        public string Margin(string type, int size)
        {
            return $"{ClassPrefix}!-margin-{type}-{size}";
        }
    }
}

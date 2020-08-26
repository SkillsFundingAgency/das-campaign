using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css
{
    public class DefaultGridCssViewModel : ICssGridViewModel
    {
        public DefaultGridCssViewModel()
        {

        }
        public DefaultGridCssViewModel(string classPrefix)
        {
            ClassPrefix = classPrefix;
        }

        public string ClassPrefix { get; set; } = "govuk-";

        public string Row => $"{ClassPrefix}grid-row";
        public string ColumnOneHalf => $"{ClassPrefix}grid-column-one-half";
        public string ColumnFull => $"{ClassPrefix}grid-column-full";

    }
}

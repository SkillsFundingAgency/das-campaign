using System;
using Sfa.Das.Sas.Shared.Components.Domain;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace SFA.DAS.Campaign.Web.Models.Fat
{
    public class CampaignCssClasses : ICssViewModel
    {
        public ITableCssViewModel Table => new DefaultTableCssViewModel(ClassPrefix);
        public IUtilitiesCssViewModel UtilitiesCss => new DefaultUtilitiesCssViewModel("u");

        public IDefaultFormCssViewModel FormCss => new DefaultFormCssViewModel(ClassPrefix);
        public string ClassModifier { get; set; } = "employer";
        public string ClassPrefix { get; set; } = string.Empty;
        private string _buttonCss => $"{ClassPrefix}button";
        public string Button
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return $"{_buttonCss} button--sparks";
                }
                else
                {
                    return $"{_buttonCss} button--sparks button-{ClassModifier}";
                }
            }
        }

        public string ButtonSecondary
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return _buttonCss;
                }
                else
                {
                    return $"{_buttonCss} button-inverted";
                }
            }
        }

        public string Input => $"{ClassPrefix}input";
        public string FormGroup => $"{ClassPrefix}form-group";
        public string Link => $"{ClassPrefix}link";
        public string List
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return $"{ClassPrefix}list";
                }
                else
                {
                    return $"{ClassPrefix}list {ClassPrefix}list--{ClassModifier}";
                }
            }
        }


        public string ListBullet
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return $"{ClassPrefix}list--bullet";
                }
                else
                {
                    return $"{ClassPrefix}list--bullet list--bullet-{ClassModifier}";
                }
            }
        }
        public string ListNumber => $"{ClassPrefix}list--number";
        public string SearchList
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return $"{List} {ListNumber} das-search-results__list";
                }
                else
                {
                    return $"{List} {ListNumber} das-search-results__list das-search-results__list--{ClassModifier}";
                }
            }
        }
        public string WarningText => $"{ClassPrefix}warning-text";
        private string _heading => $"{ClassPrefix}heading";
        public string HeadingMedium => $"{_heading}-m";
        public string HeadingLarge => $"{_heading}-l";
        public string HeadingXLarge => $"{_heading}-xl";
        public string HeadingSmall => $"{_heading}-s";
        public string HeadingXSmall => $"{_heading}-xs";


        public string Details => $"{ClassPrefix}details";

        public string DetailsSummary => $"{Details}__summary";

        public string DetailsSummaryText => $"{Details}__summary-text";

        public string DetailsText => $"{Details}__text";

        public string VisuallyHidden => "visually-hidden";

        public ICssGridViewModel GridCss => new DefaultGridCssViewModel(ClassPrefix);
    }
}

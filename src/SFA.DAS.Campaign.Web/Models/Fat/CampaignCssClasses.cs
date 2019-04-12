using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.Domain.Interfaces;
using Sfa.Das.Sas.Shared.Components.Domain;

namespace SFA.DAS.Campaign.Web.Models.Fat
{
    public class CampaignCssClasses : ICssClasses
    {
        public IUtilitiesCssClasses UtilitiesCss => new DefaultUtilitiesCssClasses("u");
        public string ClassModifier { get; set; } = "employer";
        public string ClassPrefix { get; set; } = string.Empty;
        private string _buttonCss => $"{ClassPrefix}button button--sparks";
        public string Button
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClassModifier))
                {
                    return _buttonCss;
                }
                else
                {
                    return $"{_buttonCss} button-{ClassModifier}";
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
    }
}

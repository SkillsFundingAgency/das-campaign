using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.Domain.Interfaces;

namespace SFA.DAS.Campaign.Web.Models.Fat
{
    public class CampaignCssClasses : ICssClasses
    {
        public string ClassModifier { get; set; } = "employer";
        public string ClassPrefix { get; set; } = string.Empty;
        private string _buttonCss => $"{ClassPrefix}button button--sparks";
        public string ButtonCss
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

        public string InputCss => $"{ClassPrefix}input";
        public string FormGroupCss => $"{ClassPrefix}form-group";
    }
}

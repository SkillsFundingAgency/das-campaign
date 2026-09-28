using System;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class StatsSectionControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            return control.StatsSection.ToHubStatistic();
        }

        public bool IsValid(Item control)
        {
            return string.Compare(control.Type, "embedded-entry-block", StringComparison.OrdinalIgnoreCase) == 0
                   && control.StatsSection.ToHubStatistic() != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services
{
    public interface IStandardsMapper
    {
        StandardResultItem Map(ApprenticeshipSearchResultsItem item);
    }

    public class StandardsMapper : IStandardsMapper
    {
        public StandardResultItem Map(ApprenticeshipSearchResultsItem item)
        {
            return new StandardResultItem
            {
                Duration = item.Duration,
                Title = item.Title,
                Level = item.Level
            };
        }
    }
}

﻿using Ifa.Api.Model;
using SFA.DAS.Apprenticeships.Api.Types;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public class StandardsMapper : IStandardsMapper
    {
        public StandardResultItem Map(ApprenticeshipSearchResultsItem item)
        {
            return new StandardResultItem
            {
                Id = int.Parse(item.StandardId),
                Duration = item.Duration,
                Title = item.Title,
                Level = item.Level
            };
        }
        public StandardResultItem Map(ApiApprenticeshipStandard item)
        {
            try
            {
                return new StandardResultItem
                {
                    Id = item.LarsCode,
                    Duration = item.TypicalDuration,
                    Title = item.Title,
                    Level = item.Level
                };
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

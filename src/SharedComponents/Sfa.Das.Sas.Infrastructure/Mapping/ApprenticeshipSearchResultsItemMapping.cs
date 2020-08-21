using System.Linq;
using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    using Sfa.Das.Sas.ApplicationServices.Models;
    public class ApprenticeshipSearchResultsItemMapping : IApprenticeshipSearchResultsItemMapping
    {

        public ApprenticeshipSearchResultsItem Map(SFADASApprenticeshipsApiTypesV3ApprenticeshipSearchResultsItem document)
        {
            if (document != null)
            {
                var item = new ApprenticeshipSearchResultsItem
                {
                    Id = document.Id,
                    FrameworkName = document.FrameworkName,
                    Duration = document.Duration,
                    EffectiveFrom = document.EffectiveFrom,
                    EffectiveTo = document.EffectiveTo,
                    Level = document.Level,
                    Published = document.Published,
                    PathwayName = document.PathwayName,
                    Title = document.Title,
                    Keywords = document.Keywords?.Any() == true ? document.Keywords.ToList() : null,

                    JobRoles = document.JobRoles?.Any() == true ? document.JobRoles.ToList() : null,
                    ApprenticeshipType = document.ProgrammeType == SFADASApprenticeshipsApiTypesV3ApprenticeshipSearchResultsItem.ProgrammeTypeEnum.NUMBER_0 ? ApprenticeshipType.Framework : ApprenticeshipType.Standard
                };
                return item;
            }

            return null;
        }
    }
}
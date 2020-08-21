using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    using Sfa.Das.Sas.ApplicationServices.Models;

    public interface IApprenticeshipSearchResultsItemMapping
    {
        ApprenticeshipSearchResultsItem Map(SFADASApprenticeshipsApiTypesV3ApprenticeshipSearchResultsItem document);
    }
}
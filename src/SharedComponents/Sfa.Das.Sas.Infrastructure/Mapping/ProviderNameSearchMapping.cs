using System.Collections.Generic;
using System.Linq;
using Sfa.Das.FatApi.Client.Model;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    public class ProviderNameSearchMapping : IProviderNameSearchMapping
    {
        private readonly IPaginationOrientationService _orientation;

        public ProviderNameSearchMapping(IPaginationOrientationService orientation)
        {
            _orientation = orientation;
        }

        public ProviderNameSearchResultsAndPagination Map(SFADASApprenticeshipsApiTypesV3ProviderSearchResults document, string searchTerm)
        {
            var pagination = _orientation.GeneratePaginationDetails(document.PageNumber, document.PageSize, document.TotalResults);

            var results = new ProviderNameSearchResultsAndPagination()
            {
                TotalResults = document.TotalResults,
                ActualPage = document.PageNumber,
                ResultsToTake = document.PageSize,
                LastPage = pagination.LastPage,
                HasError = false,
                ResponseCode = ProviderNameSearchResponseCodes.Success,
                SearchTerm = searchTerm,
               Results = Map(document.Results)
            };

            return results;
        }

        private IEnumerable<ProviderNameSearchResult> Map(IEnumerable<SFADASApprenticeshipsApiTypesV3ProviderNameSearchResultItem> resultsToFilter)
        {
            var resultsToReturn = new List<ProviderNameSearchResult>();
            foreach (var item in resultsToFilter)
            {
                var details = new ProviderNameSearchResult
                {
                    ProviderName = item.ProviderName,
                    UkPrn = item.Ukprn,
                    Aliases = item.Aliases?.Count > 0 ? item.Aliases.ToList() : null
                };

                resultsToReturn.Add(details);
            }

            return resultsToReturn;
        }
    }
}
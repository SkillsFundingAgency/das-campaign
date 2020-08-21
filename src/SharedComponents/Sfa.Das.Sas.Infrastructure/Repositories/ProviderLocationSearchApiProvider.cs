using System;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.Infrastructure.Elasticsearch
{
    

    public sealed class ProviderLocationSearchApiProvider : IProviderLocationSearchProvider
    {
        public ProviderSearchResult<StandardProviderSearchResultsItem> SearchStandardProviders(string standardId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter)
        {
            throw new NotImplementedException();
        }

        public ProviderSearchResult<FrameworkProviderSearchResultsItem> SearchFrameworkProviders(string frameworkId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
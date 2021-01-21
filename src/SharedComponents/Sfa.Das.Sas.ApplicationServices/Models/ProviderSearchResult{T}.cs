using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class ProviderSearchResult<T> : SearchResult<T>
        where T : class
    {
        public Dictionary<string, long?> TrainingOptionsAggregation { get; set; }

        public Dictionary<string, long?> NationalProvidersAggregation { get; set; }
    }
}

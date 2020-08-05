using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{

    public sealed class GroupedProviderSearchResult<T> : SearchResult<T>
    where T : class
    {
        public bool HasNationalProviders { get; set; }
    }
}

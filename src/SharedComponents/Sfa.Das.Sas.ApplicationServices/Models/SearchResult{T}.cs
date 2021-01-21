using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{

    public class SearchResult<T>
        where T : class
    {
        public long Total { get; set; }

        public IEnumerable<T> Hits { get; set; }
    }
}

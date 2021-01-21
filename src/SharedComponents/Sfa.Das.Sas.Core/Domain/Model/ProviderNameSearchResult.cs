using System.Collections.Generic;

namespace Sfa.Das.Sas.Core.Domain.Model
{
    public class ProviderNameSearchResult
    {
        public long UkPrn { get; set; }
        public string ProviderName { get; set; }
        public List<string> Aliases { get; set; }
        public bool IsEmployerProvider { get; set; }
    }
}
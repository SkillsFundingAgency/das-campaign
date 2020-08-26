using System.Collections.Generic;

namespace Sfa.Das.Sas.Core.Domain.Model
{
    public class ProviderNameSearchItem
    {
        public int UkPrn { get; set; }
        public string ProviderName { get; set; }
        public List<string> Aliases { get; set; }
    }
}

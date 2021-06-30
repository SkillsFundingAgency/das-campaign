using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class Item
    {
        public Item()
        {
            Values = new List<string>();
            TableValue = new List<List<string>>();
        }
        public List<string> Values { get; set; }
        public string Type { get; set; }
        public List<List<string>> TableValue { get; set; }
        public EmbeddedResource EmbeddedResource { get; set; }
    }
}
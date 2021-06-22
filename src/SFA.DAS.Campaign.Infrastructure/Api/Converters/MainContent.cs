using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class MainContent
    {
        public MainContent()
        {
            Items = new List<Item>();
        }
        public List<Item> Items { get; set; }
    }
}
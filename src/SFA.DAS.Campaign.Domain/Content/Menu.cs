using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Menu : IContentType
    {
        public IEnumerable<Url> TopLevel { get; set; }
        public IEnumerable<Url> Apprentices { get; set; }
        public IEnumerable<Url> Employers { get; set; }
        public IEnumerable<Url> Influencers { get; set; }
    }
}
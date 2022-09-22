using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Panel : IContentType
    {
        public string Slug { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public List<string> ButtonStyle { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
    }
}

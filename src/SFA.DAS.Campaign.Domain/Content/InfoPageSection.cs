using Contentful.Core.Models;
using Microsoft.AspNetCore.Html;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class InfoPageSection
    {
        public string Title { get; set; }
        public IHtmlContent Body { get; set; }
        public bool ShowHeaderLink { get; set; }
    }
}

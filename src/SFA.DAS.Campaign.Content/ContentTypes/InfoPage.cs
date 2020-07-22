using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Content.ContentTypes
{
    public class InfoPage : ContentBase
    {
        public string Title { get; set; }
        public List<InfoPageSection> Sections { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public Asset HeroImage { get; set; }
        public InfoPage PreviousPage { get; set; }
        public InfoPage NextPage { get; set; }
    }
}
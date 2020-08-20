using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Navigation
    {
        public List<NavMenu> NavBarEntries { get; set; }
    }
    
    public class NavMenu
    {
        public string Title { get; set; }
        public List<NavigationPageLink> PageLinks { get; set; }

        public string TopLevelSlug { get; set; }
    }

    public class NavigationPageLink
    {
        public string Title { get; set; }
        public string PageSlug { get; set; }
    }

}

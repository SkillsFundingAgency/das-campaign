using System.Collections.Generic;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class NavigationBar : ContentBase
    {
        public List<NavMenu> NavBarEntries { get; set; }
    }

    public class NavMenu : ContentBase
    {
        public string Title { get; set; }
        public List<NavigationPageLink> PageLinks { get; set; }
    }

    public class NavigationPageLink
    {
        public string Title { get; set; }
        public string PageSlug { get; set; }
    }
}

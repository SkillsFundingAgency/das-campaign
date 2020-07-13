using System.Collections.Generic;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class NavigationBar : ContentBase
    {
        public string Hub { get; set; }
        public List<NavMenu> NavBarEntries { get; set; }
    }

    public class NavMenu : ContentBase
    {
        public string Title { get; set; }
        public List<InfoPage> Pages { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    /// <summary>
    /// A single announcement at /employers/announcements/{slug}. The parent announcements article
    /// supplies the menu, banners and the list page title used for the back link.
    /// </summary>
    public class AnnouncementPageViewModel : Page<Announcement>
    {
        public string ListPageTitle { get; set; }

        public string ListPageUrl => Announcement.ListPageUrl;

        public static AnnouncementPageViewModel Build(Announcement announcement, Page<Article> parentPage, Menu menu, IEnumerable<Banner> banners)
        {
            return new AnnouncementPageViewModel
            {
                Slug = announcement.Slug,
                Title = announcement.Headline,
                HubType = HubType.Employers,
                Content = announcement,
                MetaContent = new MetaContent
                {
                    PageTitle = announcement.Headline,
                    MetaDescription = announcement.Intro
                },
                Menu = menu,
                BannerModels = banners?.ToList() ?? new List<Banner>(),
                ListPageTitle = parentPage?.Title ?? "Announcements"
            };
        }
    }
}

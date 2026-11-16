using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Models
{
    /// <summary>
    /// The announcements list page: the Contentful-managed article at /employers/announcements
    /// provides the title, summary, body, breadcrumbs, menu and banners; the announcements come from the API.
    /// </summary>
    public class AnnouncementsPageViewModel : Page<Article>
    {
        public List<AnnouncementSummary> Announcements { get; set; } = new List<AnnouncementSummary>();

        public static AnnouncementsPageViewModel Build(Page<Article> page, IEnumerable<AnnouncementSummary> announcements)
        {
            return new AnnouncementsPageViewModel
            {
                Slug = page.Slug,
                Title = page.Title,
                MetaContent = page.MetaContent,
                HubType = page.HubType,
                Content = page.Content,
                RelatedPages = page.RelatedPages,
                Attachments = page.Attachments,
                Breadcrumbs = page.Breadcrumbs,
                Menu = page.Menu,
                BannerModels = page.BannerModels?.ToList() ?? new List<Domain.Content.HtmlControl.Banner>(),
                Announcements = announcements?.ToList() ?? new List<AnnouncementSummary>()
            };
        }
    }
}

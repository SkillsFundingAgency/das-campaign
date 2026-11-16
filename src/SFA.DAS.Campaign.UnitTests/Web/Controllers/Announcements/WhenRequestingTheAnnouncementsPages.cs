using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Controllers;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Announcements
{
    public class WhenRequestingTheAnnouncementsPages
    {
        [Test, RecursiveMoqAutoData]
        public async Task And_The_Contentful_Page_Exists_Then_The_List_Page_Is_Returned_With_The_Announcements(
            GetArticleQueryResult<Domain.Content.Article> articleResult,
            GetAnnouncementsQueryResult announcementsResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, articleResult, false);
            mockMediator.Setup(o => o.Send(It.Is<GetAnnouncementsQuery>(q => !q.Preview), It.IsAny<CancellationToken>())).ReturnsAsync(announcementsResult);
            SetupController(controller);

            var result = await controller.Index(false, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Announcements/Index.cshtml");
            var model = (AnnouncementsPageViewModel)result.Model;
            model.Title.Should().Be(articleResult.Page.Title);
            model.Content.Should().BeSameAs(articleResult.Page.Content);
            model.Menu.Should().BeSameAs(articleResult.Page.Menu);
            model.Announcements.Should().BeEquivalentTo(announcementsResult.Announcements);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Contentful_Page_Does_Not_Exist_Then_The_List_Page_Is_Not_Found(
            GetAnnouncementsQueryResult announcementsResult,
            GetSiteMapQueryResult<SiteMap> sitemapResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, new GetArticleQueryResult<Domain.Content.Article>(), false);
            mockMediator.Setup(o => o.Send(It.IsAny<GetAnnouncementsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(announcementsResult);
            mockMediator.Setup(o => o.Send(It.IsAny<GetSiteMapQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(sitemapResult);
            SetupController(controller);

            var result = await controller.Index(false, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Error/PageNotFound.cshtml");
            controller.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Preview_Is_Requested_Then_Both_Queries_Are_Sent_With_Preview(
            GetArticleQueryResult<Domain.Content.Article> articleResult,
            GetAnnouncementsQueryResult announcementsResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, articleResult, true);
            mockMediator.Setup(o => o.Send(It.Is<GetAnnouncementsQuery>(q => q.Preview), It.IsAny<CancellationToken>())).ReturnsAsync(announcementsResult);
            SetupController(controller);

            var result = await controller.Index(true, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Announcements/Index.cshtml");
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Announcement_Exists_Then_The_Announcement_Page_Is_Returned(
            string slug,
            GetArticleQueryResult<Domain.Content.Article> articleResult,
            GetAnnouncementQueryResult announcementResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, articleResult, false);
            mockMediator.Setup(o => o.Send(It.Is<GetAnnouncementQuery>(q => q.Slug == slug && !q.Preview), It.IsAny<CancellationToken>())).ReturnsAsync(announcementResult);
            SetupController(controller);

            var result = await controller.Announcement(slug, false, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Announcements/Announcement.cshtml");
            var model = (AnnouncementPageViewModel)result.Model;
            model.Content.Should().BeSameAs(announcementResult.Announcement);
            model.Title.Should().Be(announcementResult.Announcement.Headline);
            model.Menu.Should().BeSameAs(articleResult.Page.Menu);
            model.ListPageTitle.Should().Be(articleResult.Page.Title);
            model.ListPageUrl.Should().Be("/employers/announcements");
            mockMediator.Verify(o => o.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Parent_Page_Is_Missing_Then_The_Menu_And_Banners_Come_From_Static_Content(
            string slug,
            GetAnnouncementQueryResult announcementResult,
            GetMenuQueryResult<Menu> menuResult,
            GetBannerQueryResult<BannerContentType> bannerResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, new GetArticleQueryResult<Domain.Content.Article>(), false);
            mockMediator.Setup(o => o.Send(It.IsAny<GetAnnouncementQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(announcementResult);
            mockMediator.Setup(o => o.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(menuResult);
            mockMediator.Setup(o => o.Send(It.IsAny<GetBannerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(bannerResult);
            SetupController(controller);

            var result = await controller.Announcement(slug, false, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Announcements/Announcement.cshtml");
            var model = (AnnouncementPageViewModel)result.Model;
            model.Menu.Should().BeSameAs(menuResult.Page.Menu);
            model.BannerModels.Should().BeEquivalentTo(bannerResult.Page.BannerModels);
            model.ListPageTitle.Should().Be("Announcements");
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Announcement_Does_Not_Exist_Then_The_Announcement_Page_Is_Not_Found(
            string slug,
            GetArticleQueryResult<Domain.Content.Article> articleResult,
            GetSiteMapQueryResult<SiteMap> sitemapResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AnnouncementsController controller)
        {
            SetupArticle(mockMediator, articleResult, false);
            mockMediator.Setup(o => o.Send(It.IsAny<GetAnnouncementQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetAnnouncementQueryResult());
            mockMediator.Setup(o => o.Send(It.IsAny<GetSiteMapQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(sitemapResult);
            SetupController(controller);

            var result = await controller.Announcement(slug, false, CancellationToken.None) as ViewResult;

            result.AssertThatTheReturnedViewIsCorrect("~/Views/Error/PageNotFound.cshtml");
            controller.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        private static void SetupArticle(Mock<IMediator> mockMediator, GetArticleQueryResult<Domain.Content.Article> articleResult, bool preview)
        {
            mockMediator.Setup(o => o.Send(It.Is<GetArticleQuery>(q =>
                    q.Hub == AnnouncementsController.Hub
                    && q.Slug == AnnouncementsController.Slug
                    && q.Preview == preview), It.IsAny<CancellationToken>()))
                .ReturnsAsync(articleResult);
        }

        private static void SetupController(AnnouncementsController controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }
    }
}

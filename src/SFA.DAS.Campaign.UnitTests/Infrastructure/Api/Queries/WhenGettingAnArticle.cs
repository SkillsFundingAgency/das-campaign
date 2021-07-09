using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAnArticle
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_The_Article_Is_Returned_From_The_Preview_Api_if_Config_And_Param_Set(
            GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            SetupMockConfig(config);
            query.Preview = true;
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_Preview_Is_Disabled_Then_The_Article_Is_Returned_From_The_Api(
            bool preview, GetArticleQuery query, Page<Article> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetArticleQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);
            
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
            client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_Then_The_Article_Is_Returned_From_The_Api_With_The_Menu(
            bool preview, GetArticleQuery query, Page<Article> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetArticleQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);

            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
            client.Verify(o => o.Get<Page<Menu>>(It.IsAny<GetMenuRequest>()), Times.Once);
            client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_Then_No_Article_Is_Returned(
            GetArticleQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetArticleQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Menu>>(It.Is<GetMenuRequest>(r => r.GetUrl == $"menu"))).ReturnsAsync((Page<Menu>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_For_Article_Page_Then_Landing_Page_Is_Returned(
            GetLandingPageQuery query, Page<LandingPage> pageResponse, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetLandingPageQueryHandler landingPageHandler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);

            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPagePreviewRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}"))).ReturnsAsync(pageResponse);

            var actual = await landingPageHandler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true)
        {
            config.Object.Value.AllowPreview = allowPreview;
        }
    }
}

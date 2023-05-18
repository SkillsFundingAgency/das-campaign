using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAnArticle
    {
        [Test]
        [MoqInlineAutoData(true, true)]
        [MoqInlineAutoData(true, false)]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsTrue_ThenTheArticleIsReturnedFromThePreviewAPI(
            bool allowPreview, bool previewWanted, GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, true);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsWanted_ThenThePanelIsReturnedFromThePreviewAPI(
           GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
                client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsNotWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            query.Preview = false;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
                client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsNotWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetArticleQuery query, Page<Article> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticleQueryHandler handler)
        {
            query.Preview = false;
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
                client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test]
        [RecursiveMoqInlineAutoData(true, false)]
        [RecursiveMoqInlineAutoData(false, false)]
        public async Task AndTheApiIsCalledWithTheValidRequestParameters_ThenTheArticleIsReturnedFromTheApiWithTheMenu(
            bool previewWanted, bool allowPreview, GetArticleQuery query, Page<Article> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetArticleQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);

            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);

            client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
            actual.Page.Content.Should().NotBeNull();
            actual.Page.Menu.Should().NotBeNull();
            actual.Page.Menu.Apprentices.Should().NotBeNullOrEmpty();
            actual.Page.Menu.Influencers.Should().NotBeNullOrEmpty();
            actual.Page.Menu.TopLevel.Should().NotBeNullOrEmpty();
            actual.Page.Menu.Employers.Should().NotBeNullOrEmpty();
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndTheApiIsCalledWithInvalidRequestParameters_ThenNoArticleIsReturned(
            GetArticleQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetArticleQueryHandler handler)
        {
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Menu>>(It.Is<GetMenuRequest>(r => r.GetUrl == $"menu"))).ReturnsAsync((Page<Menu>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndTheApiIsCalledWithInvalidRequestParametersForArticlePage_ThenLandingPageIsReturned(
            GetLandingPageQuery query, Page<LandingPage> pageResponse, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetLandingPageQueryHandler landingPageHandler)
        {
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);

            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPagePreviewRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}"))).ReturnsAsync(pageResponse);

            var actual = await landingPageHandler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<Article>>(It.IsAny<GetArticlesPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true, bool forcePreview = false)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.CodeAnalysis.Options;
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
    public class WhenGettingAnArticlePreview
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_The_Article_Is_Returned_From_The_Preview_Api(
            GetArticlePreviewQuery query, Page<Article> response, Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetArticlePreviewQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_Preview_Is_Disabled_Then_The_Article_Is_Returned_From_The_Api(
            GetArticlePreviewQuery query, Page<Article> response, [Frozen] Mock<IApiClient> client, Mock<IOptions<CampaignConfiguration>> config, GetArticlePreviewQueryHandler handler)
        {
            SetupMockConfig(config, false);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_Then_No_Article_Is_Returned(
            GetArticlePreviewQuery query, [Frozen] Mock<IApiClient> client, Mock<IOptions<CampaignConfiguration>> config, GetArticlePreviewQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesPreviewRequest>(r => r.GetUrl == $"article/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);
            client.Setup(o => o.Get<Page<Article>>(It.Is<GetArticlesRequest>(r => r.GetUrl == $"article/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<Article>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true)
        {
            var campaignConfiguration = new CampaignConfiguration
            {
                AllowPreview = allowPreview,
            };

            config.Setup(o => o.Value).Returns(campaignConfiguration);
        }
    }
}

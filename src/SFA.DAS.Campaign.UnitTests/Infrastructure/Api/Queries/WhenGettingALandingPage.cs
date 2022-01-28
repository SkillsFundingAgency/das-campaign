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
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingALandingPage
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_The_Landing_Page_Is_Returned_From_The_Preview_Api_if_Config_And_Param_Set(
            GetLandingPageQuery query, Page<LandingPage> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetLandingPageQueryHandler handler)
        {
            SetupMockConfig(config);
            query.Preview = true;
            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPagePreviewRequest>(r => r.GetUrl == $"landingpage/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPagePreviewRequest>(r => r.GetUrl == $"landingpage/preview/{query.Hub}/{query.Slug}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_Preview_Is_Disabled_Then_The_Landing_Page_Is_Returned_From_The_Api(
            bool preview, GetLandingPageQuery query, Page<LandingPage> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetLandingPageQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);
            
            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPageRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<LandingPage>>(It.Is<GetLandingPageRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}")), Times.Once);
            client.Verify(o => o.Get<Page<LandingPage>>(It.IsAny<GetLandingPagePreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_Then_The_Landing_Page_Is_Returned_From_The_Api_With_The_Menu(
            bool preview, GetLandingPageQuery query, Page<LandingPage> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetLandingPageQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);

            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPageRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<LandingPage>>(It.Is<GetLandingPageRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}")), Times.Once);
            client.Verify(o => o.Get<Page<LandingPage>>(It.IsAny<GetLandingPagePreviewRequest>()), Times.Never);
            
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
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_Then_No_Landing_Page_Is_Returned(
            GetLandingPageQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetLandingPageQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPagePreviewRequest>(r => r.GetUrl == $"landingpage/preview/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<LandingPage>)null);
            client.Setup(o => o.Get<Page<LandingPage>>(It.Is<GetLandingPageRequest>(r => r.GetUrl == $"landingpage/{query.Hub}/{query.Slug}"))).ReturnsAsync((Page<LandingPage>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true)
        {
            config.Object.Value.AllowPreview = allowPreview;
        }
    }
}

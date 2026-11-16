using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Controllers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Home
{
    public class WhenRequestingTheHomepage
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Homepage_Content_Menu_And_Banners_Are_Returned(
            GetHomepageQueryResult homepageResult,
            GetMenuQueryResult<Menu> menuResult,
            GetBannerQueryResult<BannerContentType> bannerResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HomeController controller)
        {
            SetupMediator(mockMediator, homepageResult, menuResult, bannerResult, false);

            var controllerResult = await controller.Index(false, CancellationToken.None) as ViewResult;

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheObjectValueIsValid<Page<Homepage>>();
            var page = (Page<Homepage>)controllerResult.Model;
            page.Content.Should().BeSameAs(homepageResult.Homepage);
            page.Menu.Should().BeSameAs(menuResult.Page.Menu);
            page.BannerModels.Should().BeSameAs(bannerResult.Page.BannerModels);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Preview_Is_Requested_Then_The_Preview_Flag_Is_Passed_To_The_Query(
            GetHomepageQueryResult homepageResult,
            GetMenuQueryResult<Menu> menuResult,
            GetBannerQueryResult<BannerContentType> bannerResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HomeController controller)
        {
            SetupMediator(mockMediator, homepageResult, menuResult, bannerResult, true);

            var controllerResult = await controller.Index(true, CancellationToken.None) as ViewResult;

            controllerResult.AssertThatTheObjectResultIsValid();
            mockMediator.Verify(o => o.Send(It.Is<GetHomepageQuery>(q => q.Preview), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_No_Homepage_Content_Is_Found_Then_The_Page_Is_Still_Returned(
            GetMenuQueryResult<Menu> menuResult,
            GetBannerQueryResult<BannerContentType> bannerResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HomeController controller)
        {
            SetupMediator(mockMediator, new GetHomepageQueryResult(), menuResult, bannerResult, false);

            var controllerResult = await controller.Index(false, CancellationToken.None) as ViewResult;

            controllerResult.AssertThatTheObjectResultIsValid();
            var page = (Page<Homepage>)controllerResult.Model;
            page.Content.Should().BeNull();
            page.Menu.Should().BeSameAs(menuResult.Page.Menu);
        }

        private static void SetupMediator(Mock<IMediator> mockMediator, GetHomepageQueryResult homepageResult,
            GetMenuQueryResult<Menu> menuResult, GetBannerQueryResult<BannerContentType> bannerResult, bool preview)
        {
            mockMediator.Setup(o => o.Send(It.Is<GetHomepageQuery>(q => q.Preview == preview), It.IsAny<CancellationToken>()))
                .ReturnsAsync(homepageResult);
            mockMediator.Setup(o => o.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(menuResult);
            mockMediator.Setup(o => o.Send(It.IsAny<GetBannerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bannerResult);
        }
    }
}

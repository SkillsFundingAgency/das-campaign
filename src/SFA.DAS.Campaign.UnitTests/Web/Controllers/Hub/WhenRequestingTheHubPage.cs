using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Controllers.Redesign;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Hub
{
    public class WhenRequestingTheHubPage
    {
        private const string HubName = "Apprentice";

        [Test, RecursiveMoqAutoData]
        public async Task And_Given_Valid_Hub_Then_The_Page_Is_Returned(
            GetHubQueryResult<Domain.Content.Hub> mediatorResult, [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HubController controller)
        {
            SetupMediator(mediatorResult, mockMediator, false);

            var controllerResult = await InstantiateController<ViewResult>(controller);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheObjectValueIsValid<Page<Domain.Content.Hub>>();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/Hubs/" + HubName + "Hub.cshtml");
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Given_An_Invalid_Hub_Then_The_Not_Found_Page_Is_Returned(
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HubController controller)
        {
            SetupMediator(new GetHubQueryResult<Domain.Content.Hub>(), mockMediator, false);

            var controllerResult = await InstantiateController<ViewResult>(controller);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/Error/PageNotFound.cshtml");
            mockMediator.Verify(o => o.Send(It.IsAny<GetSiteMapQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Is_Preview_Then_Given_Valid_Hub_Then_The_Page_Is_Returned(
            GetHubQueryResult<Domain.Content.Hub> mediatorResult, [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HubController controller)
        {
            SetupMediator(mediatorResult, mockMediator, true);

            var controllerResult = await InstantiateController<ViewResult>(controller, true);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheObjectValueIsValid<Page<Domain.Content.Hub>>();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/Hubs/" + HubName + "Hub.cshtml");
            mockMediator.Verify(o => o.Send(It.IsAny<GetSiteMapQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        private static void SetupMediator(GetHubQueryResult<Domain.Content.Hub> mediatorResult, Mock<IMediator> mockMediator, bool preview)
        {
            mockMediator.Setup(o => o.Send(It.Is<GetHubQuery>(r => 
                    r.Hub == HubName
                    && r.Preview == preview), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);
        }

        private static async Task<T> InstantiateController<T>(HubController controller, bool preview = false)
        {
            var controllerResult = (T)await controller.GetHubAsync(HubName, preview, CancellationToken.None);
            return controllerResult;
        }
    }
}

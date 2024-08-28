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
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.EmployerInform.FundingAndApprenticeship
{
    public class WhenFundingPostCalled
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Vm_Is_Stored_In_Session(
            LevyOptionViewModel model,
            GetMenuQueryResult<Menu> mediatorResult,
            [Frozen] Mock<IMediator> mediator,
            [Frozen] Mock<ISessionService> sessionService,
            [Greedy] FundingAnApprenticeshipController controller)
        {
            //Act
            await controller.Index(model);
            
            //Assert
            sessionService.Verify(ss => ss.Set(sessionService.Object.LevyOptionViewModelKey, model), Times.Once());
            mediator.Verify(x=>x.Send(It.IsAny<GetMenuQuery>(), CancellationToken.None), Times.Never);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_If_There_Is_An_Error_The_Menu_Is_Built(
            LevyOptionViewModel model,
            GetMenuQueryResult<Menu> mediatorResult,
            [Frozen] Mock<IMediator> mediator,
            [Greedy] FundingAnApprenticeshipController controller)
        {
            //Arrange
            mediator.Setup(x => x.Send(It.IsAny<GetMenuQuery>(), CancellationToken.None))
                .ReturnsAsync(mediatorResult);
            controller.ModelState.AddModelError("TestError","Error");
           
            //Act
            var actual = await controller.Index(model) as ViewResult;
            
            //Assert
            mediator.Verify(x=>x.Send(It.IsAny<GetMenuQuery>(), CancellationToken.None), Times.Once);
            Assert.That(actual, Is.Not.Null);
            var actualModel = actual.Model as LevyOptionViewModel;
            Assert.That(actualModel, Is.Not.Null);
            actualModel.Menu.Should().BeEquivalentTo(mediatorResult.Page.Menu);
        }
    }
}
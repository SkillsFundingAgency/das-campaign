using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Campaign.Web.Controllers.Redesign;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Article
{
    public class WhenRequestingTheArticlePage
    {
        private const string HubName = "hub";
        private const string SlugName = "slug";

        [Test, RecursiveMoqAutoData]
        public async Task And_Given_Valid_Hub_And_Slug_Then_The_Page_Is_Returned(
            GetArticleQueryResult<Domain.Content.Article> mediatorResult, [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ArticleController controller)
        {
            SetupMediator(mediatorResult, mockMediator, false);

            var controllerResult = await InstantiateController<ViewResult>(controller);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheObjectValueIsValid<Page<Domain.Content.Article>>();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/CMS/Article.cshtml");
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Given_An_Invalid_Hub_And_Slug_Then_The_Not_Found_Page_Is_Returned(
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ArticleController controller)
        {
            SetupMediator(new GetArticleQueryResult<Domain.Content.Article>(), mockMediator, false);

            var controllerResult = await InstantiateController<ViewResult>(controller);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/Error/PageNotFound.cshtml");
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Is_Preview_Then_Given_Valid_Hub_And_Slug_Then_The_Page_Is_Returned(
            GetArticleQueryResult<Domain.Content.Article> mediatorResult, [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ArticleController controller)
        {
            SetupMediator(mediatorResult, mockMediator, true);

            var controllerResult = await InstantiateController<ViewResult>(controller, true);

            controllerResult.AssertThatTheObjectResultIsValid();
            controllerResult.AssertThatTheObjectValueIsValid<Page<Domain.Content.Article>>();
            controllerResult.AssertThatTheReturnedViewIsCorrect("~/Views/CMS/Article.cshtml");
        }

        private static void SetupMediator(GetArticleQueryResult<Domain.Content.Article> mediatorResult, Mock<IMediator> mockMediator, bool preview)
        {
            mockMediator.Setup(o => o.Send(It.Is<GetArticleQuery>(r => 
                    r.Hub == HubName 
                    && r.Slug == SlugName
                    && r.Preview == preview), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);
        }

        private static async Task<T> InstantiateController<T>(ArticleController controller, bool preview = false)
        {
            var controllerResult = (T)await controller.GetArticleAsync(HubName, SlugName, preview, CancellationToken.None);
            return controllerResult;
        }
    }
}

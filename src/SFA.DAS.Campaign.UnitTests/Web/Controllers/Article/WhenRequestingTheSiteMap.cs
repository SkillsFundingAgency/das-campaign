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
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Controllers.Redesign;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Article
{
    public class WhenRequestingTheSiteMap
    {
        [Test, RecursiveMoqAutoData]
        public async Task The_Site_Map_Is_Returned(
            GetSiteMapQueryResult<SiteMap> mediatorResult, [Frozen] Mock<IMediator> mockMediator,
            [Greedy] HomeController controller)
        {
            mockMediator.Setup(o => o.Send(It.IsAny<GetSiteMapQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mediatorResult);

            var controllerResult = await controller.SiteMap() as ContentResult;

            controllerResult.Should().NotBeNull();
            controllerResult.ContentType.Should().Be("application/xml");
            controllerResult.Content.Should().NotBeNullOrWhiteSpace();
        }
    }
}

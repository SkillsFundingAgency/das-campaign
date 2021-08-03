using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MediatR;
using Moq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;

namespace SFA.DAS.Campaign.UnitTests
{
    public static class TestMediatorExtensions
    {
        public static void SetupMockMediator(this Mock<IMediator> mediator)
        {
            mediator.Setup(o => o.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetMenuQueryResult<Menu>()
            {
                Page = new Page<Menu>()
                {
                    Menu = new Menu()
                }
            });

            mediator.Setup(o => o.Send(It.IsAny<GetBannerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetBannerQueryResult<BannerContentType>()
            {
                Page = new Page<BannerContentType>()
                {
                    BannerModels = new List<Banner>()
                }
            });
        }
    }
}

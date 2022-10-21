﻿using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandards;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Web.Controllers;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.Employer
{
    public class WhenRequestingTheUnderstandingApprenticeshipBenefitsAndTrainingPage
    {
        private EmployerController _controller;
        private Mock<IOptions<CampaignConfiguration>> _configuration;
        private Mock<IMediator> _mediator;

        private Page<StaticContent> _staticContent;
        private GetMenuQueryResult<Menu> _menu;
        private GetBannerQueryResult<BannerContentType> _banner;
        private GetPanelQueryResult _panelResult1;
        private GetPanelQueryResult _panelResult2;
        private GetStandardsQueryResult _standards;
        private List<StandardResponse> _standardsResult;

        private const string calculationPanel1Slug = "are-you-ready-to-get-going";
        private const string calculationPanel2Slug = "future-proof-your-business";

        [SetUp]
        public void Arrange()
        {
            var _fixture = new Fixture();
            _fixture.Customizations.Add(
            new TypeRelay(
            typeof(IHtmlControl),
            typeof(GetMenuQueryResult<Menu>)));
            _fixture.Customizations.Add(
            new TypeRelay(
            typeof(IHtmlControl),
            typeof(GetBannerQueryResult<BannerContentType>)));
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _menu = _fixture.Create<GetMenuQueryResult<Menu>>();
            _banner = _fixture.Create<GetBannerQueryResult<BannerContentType>>();
            _panelResult1 = _fixture.Create<GetPanelQueryResult>();
            _panelResult2 = _fixture.Create<GetPanelQueryResult>();
            _standards = _fixture.Create<GetStandardsQueryResult>();

            _configuration = new Mock<IOptions<CampaignConfiguration>>();
            _mediator = new Mock<IMediator>();
            _mediator.SetupMockMediator();
            _controller = new EmployerController(_configuration.Object, _mediator.Object);

            _mediator.Setup(p => p.Send(It.IsAny<GetStandardsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_standards);
            _mediator.Setup(p => p.Send(It.Is<GetPanelQuery>(m => m.Slug == calculationPanel1Slug), It.IsAny<CancellationToken>())).ReturnsAsync(_panelResult1);
            _mediator.Setup(p => p.Send(It.Is<GetPanelQuery>(m => m.Slug == calculationPanel2Slug), It.IsAny<CancellationToken>())).ReturnsAsync(_panelResult2);
            _mediator.Setup(p => p.Send(It.IsAny<GetMenuQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_menu);
            _mediator.Setup(p => p.Send(It.IsAny<GetBannerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_banner);
            _staticContent = new Page<StaticContent>
            {
                Menu = _menu.Page.Menu,
                BannerModels = _banner.Page.BannerModels
            };
            _standardsResult = _standards.Standards.Select(s => new StandardResponse { Title = s.Title, LarsCode = s.LarsCode, Level = s.Level, StandardUId = s.StandardUId}).ToList();
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_UnderstandingApprenticeshipsBenefitsAndFunding_Page_Is_Returned(bool preview)
        {
            var result = await _controller.ApprenticeshipBenefitsAndFunding(preview);

            var viewResult = result as ViewResult;

            Assert.AreEqual(null, viewResult.ViewName);
            var actualModel = viewResult.Model as ApprenticeshipTrainingAndBenefitsViewModel;
            Assert.IsNotNull(actualModel);
            actualModel.Standards.Should().BeEquivalentTo(_standardsResult);
            actualModel.Panel1.Should().BeEquivalentTo(_panelResult1.Panel);
            actualModel.Panel2.Should().BeEquivalentTo(_panelResult2.Panel);
            actualModel.Menu.Should().BeEquivalentTo(_menu.Page.Menu);
            actualModel.BannerModels.Should().BeEquivalentTo(_banner.Page.BannerModels);
        }
    }
}

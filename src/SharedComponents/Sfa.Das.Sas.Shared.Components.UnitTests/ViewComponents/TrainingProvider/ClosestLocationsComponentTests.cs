using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.ViewComponents.Basket
{
    [TestFixture]
    public class ClosestLocationsComponentTests : ViewComponentTestsBase
    {
        private const string APPRENTICESHIP_ID = "123";
        private const int UKPRN = 12345678;
        private const string POSTCODE = "SW1A 1AA";
        private const int LOCATION_ID = 4444;

        private Mock<ITrainingProviderOrchestrator> _mockTrainingProviderOrchestrator;
        private ClosestLocationsViewComponent _sut;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            var orchestratorResult = new ClosestLocationsViewModel
            {
                ApprenticeshipId = APPRENTICESHIP_ID,
                Ukprn = UKPRN,
                ProviderName = "Test Provider",
                LocationId = 4444,
                Locations = new List<CloseLocationViewModel>
                {
                    new CloseLocationViewModel { LocationId = 12, Distance = 1.22d, AddressWithoutPostCode = "No 1, The Street, Somewhere", PostCode = "AB12 3FG" },
                    new CloseLocationViewModel { LocationId = 23, Distance = 3.44d, AddressWithoutPostCode = "", PostCode = "SW12 4ED" }
                }
            };

            _mockTrainingProviderOrchestrator = new Mock<ITrainingProviderOrchestrator>();
            _mockTrainingProviderOrchestrator.Setup(x => x.GetClosestLocations(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE)).ReturnsAsync(orchestratorResult);

            _sut = new ClosestLocationsViewComponent(_mockTrainingProviderOrchestrator.Object)
            {
                ViewComponentContext = _viewComponentContext
            };
        }

        [Test]
        public async Task Invoke_ReturnsDefaultView()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            result.ViewName.Should().Be("../TrainingProvider/ClosestLocations/Default");
        }

        [Test]
        public async Task Invoke_ReturnsModelContainingLocations_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            result.Should().BeOfType<ViewViewComponentResult>();
            result.ViewData.Model.Should().BeAssignableTo<ClosestLocationsViewModel>();
            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Locations.Count.Should().Be(2);
        }

        [Test]
        public async Task Invoke_ReturnsModelSpecifyingApprenticeship_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.ApprenticeshipId.Should().Be(APPRENTICESHIP_ID);
        }

        [Test]
        public async Task Invoke_ReturnsModelSpecifyingProviderName_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.ProviderName.Should().Be("Test Provider");
        }

        [Test]
        public async Task Invoke_ReturnsModelSpecifyingLocationIdOfCurrentLocation_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.LocationId.Should().Be(LOCATION_ID);
        }

        [Test]
        public async Task Invoke_ReturnsModelSpecifyingUkprn_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Ukprn.Should().Be(UKPRN);
        }

        [Test]
        public async Task Invoke_ReturnsLocationWithLocatinonId_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Locations[0].LocationId.Should().Be(12);
        }

        [Test]
        public async Task Invoke_ReturnsLocationWithDistance_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Locations[0].Distance.Should().Be(1.22d);
        }

        [Test]
        public async Task Invoke_ReturnsLocationWithPostCode_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Locations[0].PostCode.Should().Be("AB12 3FG");
        }

        [Test]
        public async Task Invoke_ReturnsLocationAddressCommaDelimitedWitoutPostCode_ForGivenCriteria()
        {
            var result = await _sut.InvokeAsync(APPRENTICESHIP_ID, UKPRN, LOCATION_ID, POSTCODE) as ViewViewComponentResult;

            var model = result.ViewData.Model as ClosestLocationsViewModel;
            model.Locations[0].AddressWithoutPostCode.Should().Be("No 1, The Street, Somewhere");
        }
    }
}

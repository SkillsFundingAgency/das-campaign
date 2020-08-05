using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping.TrainingProvider
{
    [TestFixture]
    public class GivenClosestLocationResponseMappedToViewModel
    {
        private TrainingProviderClosestLocationsViewModelMapper _sut;
        private GetClosestLocationsResponse _resultsItemToMap;

        [SetUp]
        public void Setup()
        {
            _sut = new TrainingProviderClosestLocationsViewModelMapper();

            _resultsItemToMap = new GetClosestLocationsResponse()
            {
                ProviderName = "Test Provider",
                Results = new SearchResult<CloseTrainingLocation>()
                {
                    Total = 10,
                    Hits = new List<CloseTrainingLocation>()
                    {
                        new CloseTrainingLocation { Distance = 1.23, LocationId = 111, LocationName = "Location 1 Name", Address = new Core.Domain.Model.Address { Address1 = "Address 1", Address2 = "Address 2", Town = "Town", County = "County", Postcode = "AB1 2AS" } },
                        new CloseTrainingLocation { Distance = 2.45, LocationId = 222, LocationName = "Location 2 Name", Address = new Core.Domain.Model.Address { Address1 = "Address 1", Address2 = "Address 2", Town = "Town", County = "County", Postcode = "AB1 2AS" } }
                    },
                }
            };
        }

        [Test]
        public void When_Mapping_Then_ClosestLocationResponseClosestLocationsViewModel_Is_Returned()
        {
            var result = _sut.Map("123", 12345678, 333, "AB1 3DS", _resultsItemToMap);

            result.Should().BeOfType<ClosestLocationsViewModel>();
            result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map("123", 12345678, 333, "AB1 3DS", _resultsItemToMap);

            result.ApprenticeshipId.Should().Be("123");
            result.Ukprn.Should().Be(12345678);
            result.ProviderName.Should().Be("Test Provider");
            result.LocationId.Should().Be(333);
            result.PostCode.Should().Be("AB1 3DS");
            result.Locations.Count.Should().Be(2);
            result.Locations[0].LocationId.Should().Be(111);
            result.Locations[0].Distance.Should().Be(1.23);
            result.Locations[0].AddressWithoutPostCode.Should().Be("Address 1, Address 2, Town, County");
            result.Locations[0].PostCode.Should().Be("AB1 2AS");
        }
    }
}

using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping
{
    [TestFixture]
    public class GivenFatSearchResultsItemIsMappedToViewModel
    {
        private ApprenticeshipItemViewModelMapper _sut;
        private ApprenticeshipSearchResultsItem _itemToMap;
        private Mock<ICssViewModel> _cssClassMock;

        [SetUp]
        public void Setup()
        {
            _sut = new ApprenticeshipItemViewModelMapper();

            _cssClassMock = new Mock<ICssViewModel>();

            _itemToMap = new ApprenticeshipSearchResultsItem()
            {
                Duration = 24,
                Id = "123-30-20",
                Title = "Apprenticeship Title",
                Level = 4,
                LastDateForNewStarts = DateTime.Today.AddYears(1),
                EffectiveTo = DateTime.Today.AddYears(1),
                ApprenticeshipType = ApprenticeshipType.Framework
                
            };
        }

        [Test]
        public void When_Mapping_Then_FatSearchResultsItemViewModel_Is_Returned()
        {
          var result =  _sut.Map(_itemToMap);

          result.Should().BeOfType<ApprenticeshipItemViewModel>();
          result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map(_itemToMap);

            result.ApprenticeshipType.Should().Be(_itemToMap.ApprenticeshipType);
            result.Title.Should().Be(_itemToMap.Title);
            result.Duration.Should().Be(_itemToMap.Duration);
            result.EffectiveTo.Should().Be(_itemToMap.EffectiveTo);
            result.Id.Should().Be(_itemToMap.Id);
            result.Level.Should().Be(_itemToMap.Level);
        }

    }
}

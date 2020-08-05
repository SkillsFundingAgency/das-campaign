using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping
{
    [TestFixture]
    public class GivenStandardDetailsViewModelIsMappedToViewModel
    {
        private StandardsDetailsViewModelMapper _sut;
        private Standard _itemToMap;
        private IEnumerable<AssessmentOrganisation> _organisationsToMap;
        private Mock<IAssessmentOrganisationViewModelMapper> _assessmentOrganisationMapperMock;

        [SetUp]
        public void Setup()
        {
            _assessmentOrganisationMapperMock = new Mock<IAssessmentOrganisationViewModelMapper>(MockBehavior.Strict);

            _assessmentOrganisationMapperMock.Setup(s => s.Map(It.IsAny<AssessmentOrganisation>())).Returns(new AssessmentOrganisationViewModel());

            _sut = new StandardsDetailsViewModelMapper(_assessmentOrganisationMapperMock.Object);

            _itemToMap = new Standard()
            {
                StandardId = "230-2-1",
                OverviewOfRole = "overview",
                Level = 3,
                Duration = 24,
                EffectiveTo = DateTime.Now.AddMonths(18),
                ProfessionalRegistration = "Professional registration",
                MaxFunding = 3000,
                RegulatedStandard = true,
                LastDateForNewStarts = DateTime.Today.AddDays(300),
                EntryRequirements = "Entry Requirements",
                WhatApprenticesWillLearn = "What will apprentices learn",
                Qualifications = "Qualifications",
                StandardPageUri = "http://www.standard.com"

            };

            _organisationsToMap = (new List<AssessmentOrganisation>()
            {
                new AssessmentOrganisation()
                {
                    Name = "Organisation 1",
                    Email = "Org@nisation1.com",
                    Phone = "097654321",
                    Website = "http://www.organisaition1.com"
                },
                new AssessmentOrganisation()
                {
                    Name = "Organisation 2",
                    Email = "Org@nisation2.com",
                    Phone = "097654321",
                    Website = "http://www.organisaition2.com"
                }
            }).AsEnumerable();


        }

        [Test]
        public void When_Mapping_Then_FatSearchResultsItemViewModel_Is_Returned()
        {
            var result = _sut.Map(_itemToMap, _organisationsToMap);

            result.Should().BeOfType<StandardDetailsViewModel>();
            result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map(_itemToMap, _organisationsToMap);


            result.Title.Should().Be(_itemToMap.Title);
            result.Duration.Should().Be(_itemToMap.Duration);
            result.LastDateForNewStarts.Should().Be(_itemToMap.LastDateForNewStarts);
            result.Id.Should().Be(_itemToMap.StandardId);
            result.Level.Should().Be(_itemToMap.Level);
            result.Overview.Should().Be(_itemToMap.OverviewOfRole);
            result.ProfessionalRegistration.Should().BeEquivalentTo(_itemToMap.ProfessionalRegistration);
            result.EquivalentText.Should().NotBeNullOrWhiteSpace();
            result.RegulatedStandard.Should().BeTrue();
            result.EntryRequirements.Should().BeEquivalentTo(_itemToMap.EntryRequirements);
            result.Qualifications.Should().BeEquivalentTo(_itemToMap.Qualifications);
            result.WhatApprenticesWillLearn.Should().BeEquivalentTo(_itemToMap.WhatApprenticesWillLearn);
            result.StandardPageUri.Should().BeEquivalentTo(_itemToMap.StandardPageUri);

            result.AssessmentOrganisations.Should().HaveCount(_organisationsToMap.Count());
            result.AssessmentOrganisationPresent.Should().BeTrue();

            _assessmentOrganisationMapperMock.Verify(v => v.Map(It.IsAny<AssessmentOrganisation>()), Times.AtLeast(2));

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.Mapping
{
    [TestFixture]
    public class GivenFrameworkDetailsViewModelIsMappedToViewModel
    {
        private FrameworkDetailsViewModelMapper _sut;
        private Framework _itemToMap;

        [SetUp]
        public void Setup()
        {
            _sut = new FrameworkDetailsViewModelMapper();

            _itemToMap = new Framework()
            {
                FrameworkId = "230-2-1",
                FrameworkOverview = "overview",
                Level = 3,
                Duration = 24,
                EffectiveTo = DateTime.Now.AddMonths(18),
                CompletionQualifications = "completion Qualifications",
                CompetencyQualification = new List<string>() {"Competency 1", "Competency 2"},
                CombinedQualification = new List<string>() { "Combined 1", "Combined 2"},
                KnowledgeQualification = new List<string>() { "Knowledge 1", "Knowledge 2"},
                ProfessionalRegistration = "Professional registration",
                MaxFunding = 3000,
                JobRoleItems = new List<JobRoleItem>()
                {
                    new JobRoleItem()
                    {
                        Title = "Job role 1"
                    }
                }
            };
        }

        [Test]
        public void When_Mapping_Then_FatSearchResultsItemViewModel_Is_Returned()
        {
          var result =  _sut.Map(_itemToMap);

          result.Should().BeOfType<FrameworkDetailsViewModel>();
          result.Should().NotBeNull();
        }

        [Test]
        public void When_Mapping_Then_Items_Are_Mapped()
        {
            var result = _sut.Map(_itemToMap);

       
            result.Title.Should().Be(_itemToMap.Title);
            result.Duration.Should().Be(_itemToMap.Duration);
            result.EffectiveTo.Should().Be(_itemToMap.EffectiveTo);
            result.Id.Should().Be(_itemToMap.FrameworkId);
            result.Level.Should().Be(_itemToMap.Level);
            result.Overview.Should().Be(_itemToMap.FrameworkOverview);
            result.CombinedQualification.Should().BeEquivalentTo(_itemToMap.CombinedQualification);
            result.CompetencyQualification.Should().BeEquivalentTo(_itemToMap.CompetencyQualification);
            result.CompletionQualifications.Should().BeEquivalentTo(_itemToMap.CompletionQualifications);
            result.KnowledgeQualification.Should().BeEquivalentTo(_itemToMap.KnowledgeQualification);
            result.ProfessionalRegistration.Should().BeEquivalentTo(_itemToMap.ProfessionalRegistration);
            result.JobRoles.Should().BeEquivalentTo(_itemToMap.JobRoleItems.Select(d => d.Title));
            result.EquivalentText.Should().NotBeNullOrWhiteSpace();

        }

    }
}

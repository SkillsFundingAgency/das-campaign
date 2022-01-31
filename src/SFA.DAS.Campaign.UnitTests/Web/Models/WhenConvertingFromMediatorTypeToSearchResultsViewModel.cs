using System;
using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies;
using SFA.DAS.Campaign.Domain.Enums;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.UnitTests.Web.Models
{
    public class WhenConvertingFromMediatorTypeToSearchResultsViewModel
    {
        [Test, AutoData]
        public void Then_The_Values_Are_Mapped(GetVacanciesQueryResult source)
        {
            //Arrange
            source.Location.Country = "Scotland";
            foreach (var vacancy in source.Vacancies)
            {
                vacancy.VacancyUrl = $"https://{vacancy.VacancyUrl}";
            }
            
            //Act
            var actual = (SearchResultsViewModel)source;

            actual.TotalResults.Should().Be(source.TotalFound);
            actual.Location.Latitude.Should().Be(source.Location.GeoPoint.First());
            actual.Location.Longitude.Should().Be(source.Location.GeoPoint.Last());
            Enum.TryParse<Country>(source.Location.Country, out var country);
            actual.Country.Should().Be(country);
            actual.Routes.Should().BeEquivalentTo(source.Routes.Select(c=>c.Name).ToList());
            actual.Results.Should().BeEquivalentTo(source.Vacancies, options => options
                .Excluding(c=>c.Location)
                .Excluding(c=>c.VacancyUrl)
            );
            foreach (var result in actual.Results)
            {
                result.Location.Latitude.Should().Be(source.Vacancies
                    .Single(c => c.VacancyReference.Equals(result.VacancyReference)).Location.Lat);
                result.Location.Longitude.Should().Be(source.Vacancies
                    .Single(c => c.VacancyReference.Equals(result.VacancyReference)).Location.Lon);
                result.VacancyUrl.Should().BeEquivalentTo(new Uri(source.Vacancies
                    .Single(c => c.VacancyReference.Equals(result.VacancyReference)).VacancyUrl));
            }
        }

        [Test, AutoData]
        public void Then_If_No_Location_Then_Set_To_Null(GetVacanciesQueryResult source)
        {
            //Arrange
            foreach (var vacancy in source.Vacancies)
            {
                vacancy.VacancyUrl = $"https://{vacancy.VacancyUrl}";
                vacancy.Location = null;
            }
            
            //Act
            var actual = (SearchResultsViewModel)source;
            
            //Assert
            actual.Results.ToList().TrueForAll(c => c.Location == null).Should().BeTrue();
        }
    }
}
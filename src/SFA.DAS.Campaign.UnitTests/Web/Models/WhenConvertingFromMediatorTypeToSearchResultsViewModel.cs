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
            SetVacancyUrl(source);
            
            //Act
            var actual = (SearchResultsViewModel)source;

            actual.TotalResults.Should().Be(source.TotalFound);
            actual.Location.Latitude.Should().Be(source.Location.GeoPoint.First());
            actual.Location.Longitude.Should().Be(source.Location.GeoPoint.Last());
            Enum.TryParse<Country>(source.Location.Country, out var country);
            actual.Country.Should().Be(country);
            actual.CountryName.Should().Be(source.Location.Country);
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

        [Test]
        [InlineAutoData("Scotland", "https://www.apprenticeships.scot/find-a-vacancy/", Country.Scotland)]
        [InlineAutoData("Wales", "https://careerswales.gov.wales/apprenticeship-search", Country.Wales)]
        [InlineAutoData("Northern Ireland", "https://www.nidirect.gov.uk/services/search-apprenticeship-opportunities", Country.NorthernIreland)]
        [InlineAutoData("England", "Other", Country.England)]
        [InlineAutoData("something else", "Other", Country.Other)]
        public void Then_The_Apprentice_Country_Url_Is_Mapped(string country,string expectedUrl, Country expectedCountry, GetVacanciesQueryResult source)
        {
            //Arrange
            source.Location.Country = country;
            SetVacancyUrl(source);
            
            //Act
            var actual = (SearchResultsViewModel)source;
            
            //Assert
            actual.CountryUrl.Should().Be(expectedUrl);
            actual.Country.Should().Be(expectedCountry);
            actual.CountryName.Should().Be(country);
        }

        [Test, AutoData]
        public void Then_If_No_Location_Then_Set_To_Null(GetVacanciesQueryResult source)
        {
            //Arrange
            SetVacancyUrl(source, true);
            
            //Act
            var actual = (SearchResultsViewModel)source;
            
            //Assert
            actual.Results.ToList().TrueForAll(c => c.Location == null).Should().BeTrue();
        }

        private static void SetVacancyUrl(GetVacanciesQueryResult source, bool setEmptyLocation = false)
        {
            foreach (var vacancy in source.Vacancies)
            {
                vacancy.VacancyUrl = $"https://{vacancy.VacancyUrl}";
                if (setEmptyLocation)
                {
                    vacancy.Location = null;    
                }
            }
        }
    }
}
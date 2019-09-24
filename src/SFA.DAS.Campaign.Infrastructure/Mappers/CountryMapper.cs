using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Enums;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public class CountryMapper : ICountryMapper
    {
        public Country MapToCountry(string country)
        {
            switch (country)
            {
                case "England":
                    return Country.England;
                case "Wales":
                    return Country.Wales;
                case "Scotland":
                    return Country.Scotland;
                case "Northern Ireland":
                    return Country.NorthernIreland;
                default:
                    return Country.Other;
            }
        }

    }
}

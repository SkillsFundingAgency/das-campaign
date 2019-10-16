
using SFA.DAS.Campaign.Domain.Enums;

namespace SFA.DAS.Campaign.Infrastructure.Mappers
{
    public interface ICountryMapper
    {
        Country MapToCountry(string country);
    }
}

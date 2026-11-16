using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetHomepageResponse
    {
        [JsonProperty("homepage")]
        public Homepage Homepage { get; set; }
    }
}

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetStandardsBySectorRequest : IGetApiRequest
    {
        private readonly string _sector;

        public GetStandardsBySectorRequest(string sector)
        {
            _sector = sector;
        }

        public string GetUrl => $"trainingcourses?sector={_sector}";
    }
}
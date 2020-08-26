namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public class GetFrameworkProvidersResponse
    {
        public enum ResponseCodes
        {
            Success,
            NoFrameworkFound
        }

        public ResponseCodes StatusCode { get; set; }
        public string FrameworkId { get; set; }
        public string Keywords { get; set; }
        public string Postcode { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
    }
}

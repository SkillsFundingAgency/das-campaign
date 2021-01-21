namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public class GetStandardProvidersResponse
    {
        public enum ResponseCodes
        {
            Success,
            NoStandardFound
        }

        public string StandardId { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
        public string Postcode { get; set; }
        public string Keywords { get; set; }
        public ResponseCodes StatusCode { get; set; }
    }
}

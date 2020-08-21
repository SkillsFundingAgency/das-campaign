namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class ProviderStandardSearchResults : BaseProviderSearchResults
    {
        public string StandardId { get; set; }
        public int StandardLevel { get; internal set; }
        public string StandardName { get; set; }
        public string StandardResponseCode { get; set; }
    }
}
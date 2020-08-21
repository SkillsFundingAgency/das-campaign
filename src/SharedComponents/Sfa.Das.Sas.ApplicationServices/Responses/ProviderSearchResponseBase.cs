namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public abstract class ProviderSearchResponseBase<T>
    {
        public bool Success { get; set; }
        public int CurrentPage { get; set; }
        public T Results { get; set; }
        public long TotalResultsForCountry { get; set; }
        public string SearchTerms { get; set; }
        public bool ShowAllProviders { get; set; }
        public bool ShowOnlyNationalProviders { get; set; }
        public ProviderSearchResponseCodes StatusCode { get; set; }
    }
}

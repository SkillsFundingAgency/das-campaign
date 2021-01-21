namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public enum ProviderSearchResponseCodes
    {
        Success,
        InvalidApprenticeshipId,
        PostCodeInvalidFormat,
        ApprenticeshipNotFound,
        PageNumberOutOfUpperBound,
        LocationServiceUnavailable,
        ScotlandPostcode,
        WalesPostcode,
        NorthernIrelandPostcode,
        ServerError,
        PostCodeTerminated
    }
}
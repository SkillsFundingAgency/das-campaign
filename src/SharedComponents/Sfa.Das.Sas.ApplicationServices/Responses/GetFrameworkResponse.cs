using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices.Responses
{
    public class GetFrameworkResponse
    {
        public enum ResponseCodes
        {
            Success,
            InvalidFrameworkId,
            FrameworkNotFound,
            Gone
        }

        public ResponseCodes StatusCode { get; set; }

        public Framework Framework { get; set; }

        public string SearchTerms { get; set; }
    }
}

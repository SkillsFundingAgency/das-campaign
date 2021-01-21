using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public enum SitemapType
    {
        Standards,
        Frameworks,
        Providers
    }

    public class SitemapQuery : IRequest<SitemapResponse>
    {
        public SitemapType SitemapRequest { get; set; }
        public string UrlPlaceholder { get; set; }
    }
}

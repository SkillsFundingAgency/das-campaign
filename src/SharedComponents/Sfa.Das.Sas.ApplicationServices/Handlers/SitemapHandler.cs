using System;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Helpers;
    using Core.Domain.Services;
    using MediatR;
    using Queries;
    using Responses;
    using SFA.DAS.Apprenticeships.Api.Types.Providers;

    public class SitemapHandler : IRequestHandler<SitemapQuery, SitemapResponse>
    {
        private const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;
        private readonly IGetProviderDetails _getProviders;
        private readonly IUrlEncoder _urlEncoder;

        private readonly IXmlDocumentSerialiser _xmlDocumentSerialiser;

        public SitemapHandler(IGetStandards getStandards, IGetFrameworks getFrameworks, IGetProviderDetails getProviders, IUrlEncoder urlEncoder, IXmlDocumentSerialiser documentSerialiser)
        {
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _getProviders = getProviders;
            _urlEncoder = urlEncoder;
            _xmlDocumentSerialiser = documentSerialiser;
        }

        public async Task<SitemapResponse> Handle(SitemapQuery message, CancellationToken cancellationToken)
        {
            IEnumerable<string> identifiers = null;

            switch (message.SitemapRequest)
            {
                case SitemapType.Standards:
                    identifiers = GetActiveStandardDetailsInSeoFormat();
                    break;
                case SitemapType.Frameworks:
                    identifiers = GetActiveFrameworkDetailsInSeoFormat();
                    break;
                case SitemapType.Providers:
                    identifiers = GetProviderDetailsInSeoFormat();
                    break;
            }

            var sitemapContents = _xmlDocumentSerialiser.Serialise(SitemapNamespace, message.UrlPlaceholder, identifiers);

            return new SitemapResponse
            {
                Content = sitemapContents
            };
        }

        private IEnumerable<string> GetActiveFrameworkDetailsInSeoFormat()
        {
            var frameworks = _getFrameworks.GetAllFrameworks().Where(s => CheckActive(s.EffectiveFrom, s.EffectiveTo));

            return BuildFrameworkSitemap(frameworks);
        }

        private IEnumerable<string> GetActiveStandardDetailsInSeoFormat()
        {
            var standards = _getStandards.GetAllStandards().Where(s => s.IsPublished && CheckActive(s.EffectiveFrom, s.EffectiveTo));

            return BuildStandardSitemap(standards);
        }

        private IEnumerable<string> GetProviderDetailsInSeoFormat()
        {
            var providersExcludingEmployerProviders = _getProviders.GetAllProviders().Where(x => x.IsEmployerProvider == false);

            return BuildProviderSitemapFromProviders(providersExcludingEmployerProviders);
        }

        private IEnumerable<string> BuildFrameworkSitemap(IEnumerable<Framework> frameworks)
        {
            foreach (var framework in frameworks)
            {
                var title = EncodeTitle(framework);
                yield return GetSeoFormat(framework.FrameworkId, title);
            }
        }

        private IEnumerable<string> BuildStandardSitemap(IEnumerable<Standard> standards)
        {
            foreach (var standard in standards)
            {
                var title = EncodeTitle(standard);
                yield return GetSeoFormat(standard.StandardId, title);
            }
        }

        private IEnumerable<string> BuildProviderSitemapFromProviders(IEnumerable<ProviderSummary> providers)
        {
            foreach (var provider in providers)
            {
                var encodedProviderName = _urlEncoder.EncodeTextForUri(provider.ProviderName);
                yield return $@"{provider.Ukprn}/{encodedProviderName}";
            }
        }

        private string GetSeoFormat(string id, string title)
        {
            return string.IsNullOrEmpty(title) ? $"{id}" : $"{id}/{title}";
        }

        private string EncodeTitle(IApprenticeshipProduct product)
        {
            return _urlEncoder.EncodeTextForUri(product.Title);
        }

        private static bool CheckActive(DateTime? effectiveFrom, DateTime? effectiveTo)
        {
            var effectiveFromInThePast = effectiveFrom.HasValue && effectiveFrom.Value <= DateTime.Now;
            var effectiveToIntheFuture = !effectiveTo.HasValue || effectiveTo.Value > DateTime.Now;

            return effectiveFromInThePast && effectiveToIntheFuture;
        }
    }
}

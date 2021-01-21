using Refit;
using Sfa.Das.FatApi.Client.Api;
using SFA.DAS.Providers.Api.Client;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Infrastructure.Providers;

namespace Sfa.Das.Sas.Infrastructure.DependencyResolution
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using ApplicationServices;
    using ApplicationServices.Settings;
    using Core.Configuration;
    using Core.Domain.Repositories;
    using Core.Domain.Services;
    using Mapping;
    using PostCodeIo;
    using Repositories;
    using Settings;
    using SFA.DAS.Apprenticeships.Api.Client;
    using SFA.DAS.AssessmentOrgs.Api.Client;
    using StructureMap;

    public sealed class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<SFA.DAS.NLog.Logger.ILog>().Use(x => new SFA.DAS.NLog.Logger.NLogLogger(
                x.ParentType,
                x.GetInstance<SFA.DAS.NLog.Logger.IRequestContext>(),
                GetProperties()
            )).AlwaysUnique();
            For<IConfigurationSettings>().Use<ApplicationSettings>();
            For<IFatConfigurationSettings>().Use<FatSettings>();
            For<ICookieSettings>().Use<CookieSettings>();
            For<ILookupLocations>().Use<PostCodesIoLocator>();

            For<IApprenticeshipSearchProvider>().Use<ApprenticeshipsSearchApiProvider>();
            For<IGetFrameworks>().Use<FrameworkApiRepository>();
            For<IGetStandards>().Use<StandardApiRepository>();
            For<IGetAssessmentOrganisations>().Use<AssessmentOrganisationApiRepository>();
            For<IApprenticeshipProviderRepository>().Use<ApprenticeshipProviderApiRepository>();
           // For<IProviderLocationSearchProvider>().Use<ProviderLocationSearchApiProvider>();

            For<IStandardApiClient>().Use<StandardApiClient>().Ctor<string>("baseUri").Is(new FatSettings().FatApiBaseUrl);
            For<IFrameworkApiClient>().Use<FrameworkApiClient>().Ctor<string>("baseUri").Is(new FatSettings().FatApiBaseUrl);
            For<IAssessmentOrgsApiClient>().Use<AssessmentOrgsApiClient>().Ctor<string>("baseUri").Is(new FatSettings().FatApiBaseUrl);
            For<IStandardMapping>().Use<StandardMapping>();
            For<IFrameworkMapping>().Use<FrameworkMapping>();
            For<IProviderMapping>().Use<ProviderMapping>();
            For<IAssessmentOrganisationMapping>().Use<AssessmentOrganisationMapping>();
            For<IProviderNameSearchMapping>().Use<ProviderNameSearchMapping>();
            For<IApprenticeshipSearchResultsMapping>().Use<ApprenticeshipSearchResultsMapping>();
            For<IApprenticeshipSearchResultsItemMapping>().Use<ApprenticeshipSearchResultsItemMapping>();
            For<ISearchResultsMapping>().Use<SearchResultsMapping>();
            For<IProviderSearchResultsMapper>().Use<ProviderSearchResultsMapper>();

            For<IPaginationOrientationService>().Use<PaginationOrientationService>();

            For<ISearchApi>().Use(RestService.For<ISearchApi>(new FatSettings().FatApiBaseUrl)).Singleton();
            For<ISearchV3Api>().Use(RestService.For<ISearchV3Api>(new FatSettings().FatApiBaseUrl)).Singleton();
            For<ISearchV4Api>().Use(RestService.For<ISearchV4Api>(new FatSettings().FatApiBaseUrl)).Singleton();
            For<IProvidersVApi>().Use(RestService.For<IProvidersVApi>(new FatSettings().FatApiBaseUrl)).Singleton();

            For<IProviderApiClient>().Use(new ProviderApiClient(new FatSettings().FatApiBaseUrl));
            For<IProviderSearchProvider>().Use<ProviderApiRepository>();

            For<IPostcodeIOConfigurationSettings>().Use<ApplicationSettings>();

            For<IPaginationOrientationService>().Use<PaginationOrientationService>();
        }

        private IDictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            properties.Add("Version", GetVersion());
            return properties;
        }

        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}

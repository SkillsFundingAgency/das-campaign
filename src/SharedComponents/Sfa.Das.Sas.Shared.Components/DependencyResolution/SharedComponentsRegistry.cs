using System;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Sfa.Das.FatApi.Client.Api;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Http;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.ApplicationServices.Settings;
using Sfa.Das.Sas.ApplicationServices.Validators;
using Sfa.Das.Sas.Core;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Core.Domain.Repositories;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Infrastructure.Mapping;
using Sfa.Das.Sas.Infrastructure.PostCodeIo;
using Sfa.Das.Sas.Infrastructure.Providers;
using Sfa.Das.Sas.Infrastructure.Repositories;
using Sfa.Das.Sas.Shared.Basket;
using Sfa.Das.Sas.Shared.Components.Configuration;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.AssessmentOrgs.Api.Client;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Providers.Api.Client;

namespace Sfa.Das.Sas.Shared.Components.DependencyResolution
{
    public static class SharedComponentsRegistry
    {
        public static void AddFatSharedComponents(this IServiceCollection services, FatSharedComponentsConfiguration configuration)
        {
            services.AddFavouritesBasket(configuration.BasketRedisConnectionString, configuration.BasketSlidingExpiryDays);

            services.AddTransient<SFA.DAS.NLog.Logger.ILog, SFA.DAS.NLog.Logger.NLogLogger>(x => new NLogLogger());

            services.AddMediatR(typeof(ApprenticeshipSearchQuery));
            services.AddTransient<ICssViewModel, DefaultCssViewModel>();
            services.AddTransient<IValidation, Validation>();
            services.AddHttpContextAccessor();
            services.AddTransient<ICookieManager, CookieManager>();

            services.AddSingleton<IPostcodeIOConfigurationSettings, FatSharedComponentsConfiguration>(s => configuration);
            services.AddSingleton<IApprenticehipFavouritesBasketStoreConfig, FatSharedComponentsConfiguration>(s => configuration);
            services.AddSingleton<ICacheSettings, FatSharedComponentsConfiguration>(s => configuration);
            
            //Application DI
            AddApplicationServices(services);

            //Infrastructure DI
            AddInfrastructureServices(services);

            //Orchestrator DI
            AddOrchesratorServices(services);

            AddApiSearchServices(services, configuration);

            services.AddTransient<IApprenticeshipItemViewModelMapper, ApprenticeshipItemViewModelMapper>();
            services.AddTransient<IFatSearchResultsViewModelMapper, FatSearchResultsViewModelMapper>();
            services.AddTransient<IFrameworkDetailsViewModelMapper, FrameworkDetailsViewModelMapper>();
            services.AddTransient<IStandardDetailsViewModelMapper, StandardsDetailsViewModelMapper>();
            services.AddTransient<IAssessmentOrganisationViewModelMapper, AssessmentOrganisationViewModelMapper>();
            services.AddTransient<ITrainingProviderSearchResultsItemViewModelMapper, TrainingProviderSearchResultsItemViewModelMapper>();
            services.AddTransient<ISearchResultsViewModelMapper, SearchResultsViewModelMapper>();
            services.AddTransient<IProviderSearchResultsMapper, ProviderSearchResultsMapper>();
            services.AddTransient<ISearchResultsViewModelMapper, SearchResultsViewModelMapper>();
            services.AddTransient<IFeedbackViewModelMapper, FeedbackViewModelMapper>();
            services.AddTransient<ITrainingProviderDetailsViewModelMapper, TrainingProviderDetailsViewModelMapper>();
            services.AddTransient<ITrainingProviderClosestLocationsViewModelMapper, TrainingProviderClosestLocationsViewModelMapper>();
            services.AddTransient<IBasketViewModelMapper,BasketViewModelMapper>();
            services.AddTransient<IApprenticeshipBasketItemViewModelMapper, ApprenticeshipBasketItemViewModelMapper>();

        
            services.AddTransient<IFatSearchFilterViewModelMapper, FatSearchFilterViewModelMapper>();
            services.AddTransient<ITrainingProviderSearchFilterViewModelMapper, TrainingProviderSearchFilterViewModelMapper>();

        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IHttpGet, HttpService>();
            services.AddTransient<IApprenticeshipSearchService, ApprenticeshipSearchService>();
            services.AddTransient<IProviderSearchService, ProviderSearchService>();
            services.AddTransient<IPaginationSettings, PaginationSettings>();
            services.AddSingleton<IValidator<ProviderSearchQuery>, ProviderSearchQueryValidator>();
            services.AddSingleton<IValidator<GroupedProviderSearchQuery>, GroupedProviderSearchQueryValidator>();
            services.AddSingleton<IValidator<GetClosestLocationsQuery>, GetClosestLocationsQueryValidator>();
            services.AddTransient<AbstractValidator<ApprenticeshipProviderDetailQuery>, ApprenticeshipProviderDetailQueryValidator>();
            services.AddTransient<AbstractValidator<GetFrameworkQuery>, FrameworkQueryValidator>();
            services.AddTransient<IPostcodeService, PostcodeIoService>();
            services.AddTransient<IGetProviderDetails, ProviderApiRepository>();
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            // services.AddTransient<IConfigurationSettings, ApplicationSettings>();

            services.AddTransient<ILookupLocations, PostCodesIoLocator>();

            services.AddTransient<IGetFrameworks, FrameworkApiRepository>();
            services.AddTransient<IGetStandards, StandardApiRepository>();
            services.AddTransient<IGetAssessmentOrganisations, AssessmentOrganisationApiRepository>();
            services.AddTransient<IApprenticeshipProviderRepository, ApprenticeshipProviderApiRepository>();

            services.AddTransient<IStandardMapping, StandardMapping>();
            services.AddTransient<IFrameworkMapping, FrameworkMapping>();
            services.AddTransient<IProviderMapping, ProviderMapping>();
            services.AddTransient<IAssessmentOrganisationMapping, AssessmentOrganisationMapping>();
            services.AddTransient<IProviderNameSearchMapping, ProviderNameSearchMapping>();
            services.AddTransient<IApprenticeshipSearchResultsMapping, ApprenticeshipSearchResultsMapping>();
            services.AddTransient<IApprenticeshipSearchResultsItemMapping, ApprenticeshipSearchResultsItemMapping>();
            services.AddTransient<ISearchResultsMapping, SearchResultsMapping>();

            services.AddTransient<IPaginationOrientationService, PaginationOrientationService>();

            services.AddTransient<IRetryWebRequests, WebRequestRetryService>();
        }



        private static void AddApiSearchServices(IServiceCollection services, IFatConfigurationSettings sharedComponentsConfiguration)
        {

            services.AddTransient<IApprenticeshipSearchProvider, ApprenticeshipsSearchApiProvider>();

            services.AddTransient<IStandardApiClient, StandardApiClient>(service => new StandardApiClient(sharedComponentsConfiguration.FatApiBaseUrl));
            services.AddTransient<IFrameworkApiClient, FrameworkApiClient>(service => new FrameworkApiClient(sharedComponentsConfiguration.FatApiBaseUrl));
            services.AddTransient<IAssessmentOrgsApiClient, AssessmentOrgsApiClient>(service => new AssessmentOrgsApiClient(sharedComponentsConfiguration.FatApiBaseUrl));
            services.AddTransient<IApprenticeshipProgrammeApiClient, ApprenticeshipProgrammeApiClient>(service => new ApprenticeshipProgrammeApiClient(sharedComponentsConfiguration.FatApiBaseUrl));

          //  services.AddTransient<IProviderLocationSearchProvider, ProviderLocationSearchApiProvider>();
            services.AddTransient<IProviderSearchProvider, ProviderApiRepository>();
            services.AddTransient<IProviderApiClient, ProviderApiClient>(service => new ProviderApiClient(sharedComponentsConfiguration.FatApiBaseUrl));

            services.AddRefitClient<ISearchApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(sharedComponentsConfiguration.FatApiBaseUrl));

            services.AddRefitClient<IProvidersVApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(sharedComponentsConfiguration.FatApiBaseUrl));
            services.AddRefitClient<ISearchV3Api>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(sharedComponentsConfiguration.FatApiBaseUrl));
            services.AddRefitClient<ISearchV4Api>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(sharedComponentsConfiguration.FatApiBaseUrl));
        }

        private static void AddOrchesratorServices(IServiceCollection services)
        {
            services.AddScoped<IApprenticeshipOrchestrator, ApprenticeshipOrchestrator>();
            services.AddTransient<IFatOrchestrator, FatOrchestrator>();
            services.AddTransient<ITrainingProviderOrchestrator, TrainingProviderOrchestrator>();
            services.AddTransient<IBasketOrchestrator, BasketOrchestrator>();
        }
    }
}

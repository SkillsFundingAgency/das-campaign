using FluentValidation;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Http;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.ApplicationServices.Settings;
using Sfa.Das.Sas.ApplicationServices.Validators;
using StructureMap;
using StructureMap.Graph;

namespace Sfa.Das.Sas.ApplicationServices.DependencyResolution
{
    public sealed class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            this.Scan(
                scan =>
                {
                    scan.AssemblyContainingType<ApplicationServicesRegistry>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AddAllTypesOf(typeof(IRequestHandler<,>));
                });

            For<IHttpGet>().Use<HttpService>();
            For<IApprenticeshipSearchService>().Use<ApprenticeshipSearchService>();
            For<IProviderSearchService>().Use<ProviderSearchService>();
            For<IPaginationSettings>().Use<PaginationSettings>();
            For<AbstractValidator<ProviderSearchQuery>>().Use<ProviderSearchQueryValidator>();
            For<AbstractValidator<GroupedProviderSearchQuery>>().Use<GroupedProviderSearchQueryValidator>();
            For<AbstractValidator<ApprenticeshipProviderDetailQuery>>().Use<ApprenticeshipProviderDetailQueryValidator>();
            For<AbstractValidator<GetFrameworkQuery>>().Use<FrameworkQueryValidator>();
            For<IPostcodeService>().Use<PostcodeIoService>();
        }
    }
}

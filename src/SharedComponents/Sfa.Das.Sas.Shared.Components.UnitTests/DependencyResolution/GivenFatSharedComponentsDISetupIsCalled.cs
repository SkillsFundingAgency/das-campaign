using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Sfa.Das.Sas.ApplicationServices.Http;
using Sfa.Das.Sas.Shared.Components.DependencyResolution;
using SFA.DAS.Apprenticeships.Api.Client;
using System;
using System.Linq;
using Sfa.Das.Sas.Shared.Components.Configuration;
using Moq;
using Microsoft.Extensions.Hosting;

namespace Sfa.Das.Sas.Shared.Components.UnitTests.DependencyResolution
{
    [TestFixture]
    public class Given_Fat_Shared_Components_DI_Setup_Is_Called
    {

        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        private FatSharedComponentsConfiguration _FatConfiguration;

        private string _fatApiUrl = "http://fatApi.url";

        [SetUp]
        public void Setup()
        {
            var mockHostingEnvironment = new Mock<IHostingEnvironment>();
            mockHostingEnvironment.Setup(m => m.EnvironmentName).Returns("Development");

            _FatConfiguration = new FatSharedComponentsConfiguration()
            {
                FatApiBaseUrl = _fatApiUrl
            };

            _serviceCollection = new ServiceCollection();

            _serviceCollection.AddTransient<IHostingEnvironment>(x => mockHostingEnvironment.Object);

            _serviceCollection.AddFatSharedComponents(_FatConfiguration);

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void Then_Mediatr_DI_Is_Called()
        {
            _serviceCollection.Where(w => w.ServiceType.FullName.StartsWith("MediatR.IRequestHandler")).Should().HaveCountGreaterThan(1);
        }

        [Test]
        public void Then_StandardApiClient_is_registered()
        {
            var standardApi = _serviceProvider.GetService<IStandardApiClient>();

            standardApi.Should().NotBeNull();
            standardApi.Should().BeOfType<StandardApiClient>();
        }
        [Test]
        public void Then_FrameworkApiClient_is_registered()
        {
            var frameworkApi = _serviceProvider.GetService<IFrameworkApiClient>();

            frameworkApi.Should().NotBeNull();
            frameworkApi.Should().BeOfType<FrameworkApiClient>();
        }

        [Test]
        public void Then_HttpService_is_registered()
        {
            var httpClient = _serviceProvider.GetService<IHttpGet>();

            httpClient.Should().NotBeNull();
            httpClient.Should().BeOfType<HttpService>();
        }

       
    }
}
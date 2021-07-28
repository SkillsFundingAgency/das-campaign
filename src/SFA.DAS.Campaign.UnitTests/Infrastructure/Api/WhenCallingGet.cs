using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api
{
    public class WhenCallingGet
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_With_Authentication_Header_And_Data_Returned(
            List<string> testObject, 
            OuterApiConfiguration config,
            Mock<List<ICmsPageConverter>> converters)
        {
            //Arrange
            var configMock = new Mock<IOptions<CampaignConfiguration>>();
            config.BaseUrl = "https://test.local/";
            configMock.Setup(x => x.Value.OuterApi).Returns(config);
            var getTestRequest = new GetTestRequest();
            
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(testObject)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config.BaseUrl + getTestRequest.GetUrl, config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object, converters.Object);

            //Act
            var actual = await apiClient.Get<List<string>>(getTestRequest);
            
            //Assert
            actual.Should().BeEquivalentTo(testObject);
        }
        
        [Test, AutoData]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(
            OuterApiConfiguration config, Mock<IEnumerable<ICmsPageConverter>> converters)
        {
            //Arrange
            var configMock = new Mock<IOptions<CampaignConfiguration>>();
            config.BaseUrl = "https://test.local/";
            configMock.Setup(x => x.Value.OuterApi).Returns(config);
            var getTestRequest = new GetTestRequest();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config.BaseUrl + getTestRequest.GetUrl, config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object, converters.Object);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => apiClient.Get<List<string>>(getTestRequest));
            
        }
        
        [Test, AutoData]
        public async Task Then_If_It_Is_Not_Found_Default_Is_Returned(
            OuterApiConfiguration config, Mock<IEnumerable<ICmsPageConverter>> converters)
        {
            //Arrange
            var configMock = new Mock<IOptions<CampaignConfiguration>>();
            config.BaseUrl = "https://test.local/";
            configMock.Setup(x => x.Value.OuterApi).Returns(config);
            var getTestRequest = new GetTestRequest();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.NotFound
            };
            
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, config.BaseUrl + getTestRequest.GetUrl, config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object, converters.Object);
            
            //Act Assert
            var actual = await apiClient.Get<List<string>>(getTestRequest);

            actual.Should().BeNull();

        }

        private class GetTestRequest : IGetApiRequest
        {
            public string GetUrl => $"test-url/get";
        }
    }
    public static class MessageHandler
    {
        public static Mock<HttpMessageHandler> SetupMessageHandlerMock(HttpResponseMessage response, string url, string key)
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(c =>
                        c.Method.Equals(HttpMethod.Get)
                        && c.Headers.Contains("Ocp-Apim-Subscription-Key")
                        && c.Headers.GetValues("Ocp-Apim-Subscription-Key").First().Equals(key)
                        && c.Headers.Contains("X-Version")
                        && c.Headers.GetValues("X-Version").First().Equals("1")
                        && c.RequestUri.AbsoluteUri.Equals(url)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => response);
            return httpMessageHandler;
        }
    }
}
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Infrastructure.PostCodeIo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Core.Configuration;
    using Logging;
    using Newtonsoft.Json;
    using Sfa.Das.Sas.ApplicationServices;
    using Sfa.Das.Sas.ApplicationServices.Exceptions;
    using Sfa.Das.Sas.ApplicationServices.Models;
    using Sfa.Das.Sas.Core;
    using Sfa.Das.Sas.Core.Domain.Model;
    

    public class PostCodesIoLocator : ILookupLocations
    {
        private readonly IRetryWebRequests _retryService;
        private readonly ILog _logger;

        private readonly IPostcodeIOConfigurationSettings _applicationSettings;

        public PostCodesIoLocator(IRetryWebRequests retryService, ILog logger, IPostcodeIOConfigurationSettings applicationSettings)
        {
            _retryService = retryService;
            _logger = logger;
            _applicationSettings = applicationSettings;
        }

        public async Task<CoordinateResponse> GetLatLongFromPostCode(string postcode)
        {
            var coordinates = new Coordinate();
            var uri = new Uri(_applicationSettings.PostcodeUrl, postcode.Replace(" ", string.Empty));

            try
            {
                var stopwatch = Stopwatch.StartNew();
                var response = await _retryService.RetryWeb(() => MakeRequestAsync(uri.ToString()), CouldntConnect);
                stopwatch.Stop();
                var responseTime = stopwatch.ElapsedMilliseconds;

                if (response.IsSuccessStatusCode)
                {
                    var value = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PostCodeResponse>(value);
                    if (!result.Result.Latitude.HasValue || !result.Result.Longitude.HasValue)
                    {
                        return new CoordinateResponse
                        {
                            Coordinate = null,
                            ResponseCode = LocationLookupResponse.MissingCoordinates
                        };
                    }

                    coordinates.Lat = result.Result.Latitude.Value;
                    coordinates.Lon = result.Result.Longitude.Value;

                    SendDependencyLog(response.StatusCode, uri, responseTime);

                    var coordinateResponse = new CoordinateResponse
                    {
                        Coordinate = coordinates,
                        ResponseCode = LocationLookupResponse.Ok
                    };

                    return coordinateResponse;
                }

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    LogInformation(postcode, uri, response, responseTime, "Postcodes.IO-ServerError", "Server error trying to find postcode");

                    return new CoordinateResponse
                    {
                        Coordinate = null,
                        ResponseCode = LocationLookupResponse.ServerError
                    };
                }

                LogInformation(postcode, uri, response, responseTime, "Postcodes.IO-PostCodeNotFound", "Unable to find coordinates for postcode");

                return new CoordinateResponse
                {
                    Coordinate = null,
                    ResponseCode = LocationLookupResponse.WrongPostcode
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to connect to Postcode lookup servce. Url: {uri}");

                throw new SearchException("Unable to connect to Post Code Lookup service", ex);
            }
        }

        private void LogInformation(string postcode, Uri uri, HttpResponseMessage response, long responseTime, string identifier, string message)
        {
            var dir = new Dictionary<string, object>
            {
                { "Identifier ", identifier },
                { "Postcode", postcode },
                { "Url", uri.ToString() }
            };

            _logger.Info($"{message}: {postcode}. Service url:{uri}", dir);
            SendDependencyLog(response.StatusCode, uri, responseTime);
        }

        private void SendDependencyLog(HttpStatusCode statusCode, Uri uri, long responseTime)
        {
            var logEntry = new DependencyLogEntry
            {
                Identifier = "PostcodeIo Postcode Search",
                ResponseCode = (int)statusCode,
                ResponseTime = responseTime,
                Url = uri.ToString()
            };

            _logger.Debug("Dependency PostCodeIo", logEntry);
        }

        private void CouldntConnect(Exception ex)
        {
            _logger.Warn(string.Concat("Couldn't connect to postcode service, retrying...", ex.Message));
        }

        private async Task<HttpResponseMessage> MakeRequestAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    return await client.SendAsync(request);
                }
            }
        }
    }
}

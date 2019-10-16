using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Application.Geocode.Models;
using SFA.DAS.Campaign.Infrastructure.Geocode.Configuration;
using SFA.DAS.Campaign.Infrastructure.Services;
using SFA.DAS.Campaign.Models.Geocode;

namespace SFA.DAS.Campaign.Infrastructure.Geocode
{
    public class GeocodeService : IGeocodeService
    {
        private ILogger<IGeocodeService> _logger;
        private IRetryWebRequests _retryWebRequests;
        private IPostcodeApiConfiguration _postcodeConfiguration;

        public GeocodeService(ILogger<IGeocodeService> logger, IRetryWebRequests retryWebRequests, IPostcodeApiConfiguration postcodeConfiguration)
        {
            _logger = logger;
            _retryWebRequests = retryWebRequests;
            _postcodeConfiguration = postcodeConfiguration;
        }

        public async Task<CoordinatesResponse> GetFromPostCode(string postcode)
        {
            var coordinates = new Coordinates();
            var uri = new Uri(_postcodeConfiguration.Url + "postcodes/" + postcode.Replace(" ", string.Empty));
            
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var response = await _retryWebRequests.Retry(() => _retryWebRequests.MakeRequestAsync(uri.ToString()), CouldntConnect);
                stopwatch.Stop();
                var responseTime = stopwatch.ElapsedMilliseconds;

                if (response.IsSuccessStatusCode)
                {
                    var value = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PostCodeResponse>(value);
                    if (!result.Result.Latitude.HasValue || !result.Result.Longitude.HasValue)
                    {
                        return new CoordinatesResponse
                        {
                            Coordinates = null,
                            ResponseCode = LocationLookupResponse.MissingCoordinates
                        };
                    }

                    coordinates.Lat = result.Result.Latitude.Value;
                    coordinates.Lon = result.Result.Longitude.Value;
                   
                    SendDependencyLog(response.StatusCode, uri, responseTime);

                    var coordinateResponse = new CoordinatesResponse
                    {
                        Coordinates = coordinates,
                        Country = result.Result.Country,
                        ResponseCode = LocationLookupResponse.Ok
                    };

                    return coordinateResponse;
                }

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    LogInformation(postcode, uri, response, responseTime, "Postcodes.IO-ServerError", "Server error trying to find postcode");

                    return new CoordinatesResponse
                    {
                        Coordinates = null,
                        ResponseCode = LocationLookupResponse.ServerError
                    };
                }

                LogInformation(postcode, uri, response, responseTime, "Postcodes.IO-PostCodeNotFound", "Unable to find coordinates for postcode");

                return new CoordinatesResponse
                {
                    Coordinates = null,
                    ResponseCode = LocationLookupResponse.WrongPostcode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to connect to Postcode lookup servce. Url: {uri}");

                throw new Exception("Unable to connect to Post Code Lookup service", ex);
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

            _logger.LogInformation($"{message}: {postcode}. Service url:{uri}", dir);
            SendDependencyLog(response.StatusCode, uri, responseTime);
        }

        private void SendDependencyLog(HttpStatusCode statusCode, Uri uri, long responseTime)
        {
            var logEntry = new
            {
                Identifier = "PostcodeIo Postcode Search",
                ResponseCode = (int)statusCode,
                ResponseTime = responseTime,
                Url = uri.ToString()
            };

            _logger.LogDebug("Dependency PostCodeIo", logEntry);
        }
        private void CouldntConnect(Exception ex)
        {
            _logger.LogWarning(string.Concat("Couldn't connect to postcode service, retrying...", ex.Message));
        }
        
    }

}

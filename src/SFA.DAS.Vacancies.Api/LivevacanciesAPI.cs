// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

using SFA.DAS.Vacancies.Api.Models;

namespace VacanciesApi
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class LivevacanciesAPI : ServiceClient<LivevacanciesAPI>, ILivevacanciesAPI
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <summary>
        /// Initializes a new instance of the LivevacanciesAPI class.
        /// </summary>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling LivevacanciesAPI.Dispose(). False: will not dispose provided httpClient</param>
        public LivevacanciesAPI(HttpClient httpClient, bool disposeHttpClient) : base(httpClient, disposeHttpClient)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the LivevacanciesAPI class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public LivevacanciesAPI(params DelegatingHandler[] handlers) : base(handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the LivevacanciesAPI class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public LivevacanciesAPI(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the LivevacanciesAPI class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public LivevacanciesAPI(System.Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the LivevacanciesAPI class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public LivevacanciesAPI(System.Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        ///</summary>
        partial void CustomInitialize();
        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            BaseUri = new System.Uri("https://apis.apprenticeships.sfa.bis.gov.uk/vacancies");
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new  List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            CustomInitialize();
        }
        /// <summary>
        /// GetApprenticeshipVacancyAsync
        /// </summary>
        /// <remarks>
        /// The apprenticeship operation retrieves a single live apprenticeship vacancy
        /// using the vacancy reference number.
        ///
        /// Note that:
        ///
        /// - the vacancy reference number should be specified as a number (ie.
        /// excluding any prefix)
        /// - only live vacancies can be retrieved using this operation
        ///
        /// #### Example ####
        ///
        /// To retrieve VAC001234567:
        ///
        /// ```
        /// /apprenticeships/1234567
        /// ```
        ///
        /// #### Error codes ####
        ///
        /// The following error codes may be returned when calling this operation:
        ///
        /// | Error code  | Explanation
        /// |
        /// | ----------- |
        /// -------------------------------------------------------------- |
        /// | 30201       | Vacancy reference number must be greater than 0
        /// |
        /// | 30202       | Vacancy reference number must be a whole number
        /// |
        /// </remarks>
        /// <param name='vacancyReference'>
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> GetApprenticeshipVacancyAsync(string vacancyReference, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (vacancyReference == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "vacancyReference");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("vacancyReference", vacancyReference);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "FivebFivefZeroNineFiveeaSevenNinedNinedOneZeroFivecEightEightdfSixNine", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "v1/apprenticeships/{vacancyReference}").ToString();
            _url = _url.Replace("{vacancyReference}", System.Uri.EscapeDataString(vacancyReference));
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


            if (customHeaders != null)
            {
                foreach(var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 200 && (int)_statusCode != 400 && (int)_statusCode != 404)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<ApprenticeshipVacancy>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int)_statusCode == 400)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<BadRequestContent>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// GetTraineeshipVacancy
        /// </summary>
        /// <remarks>
        /// The traineeship operation retrieves a single live traineeship vacancy using
        /// the vacancy reference number.
        ///
        /// Note that:
        ///
        /// - the vacancy reference number should be specified as a number (ie.
        /// excluding any prefix)
        /// - only live vacancies can be retrieved using this operation
        ///
        /// #### Example ####
        ///
        /// To retrieve VAC001234567:
        ///
        /// ```
        /// /traineeships/1234567
        /// ```
        ///
        /// #### Error codes ####
        ///
        /// The following error codes may be returned when calling this operation:
        ///
        /// | Error code  | Explanation
        /// |
        /// | ----------- |
        /// -------------------------------------------------------------- |
        /// | 30401       | Vacancy reference number must be greater than 0
        /// |
        /// | 30402       | Vacancy reference number must be a whole number
        /// |
        /// </remarks>
        /// <param name='vacancyReference'>
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> GetTraineeshipVacancyAsync(string vacancyReference, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (vacancyReference == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "vacancyReference");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("vacancyReference", vacancyReference);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "FivebFivefZeroNineFiveeaSevenNinedNinedOneZeroFivecEightEightdfSixa", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "v1/traineeships/{vacancyReference}").ToString();
            _url = _url.Replace("{vacancyReference}", System.Uri.EscapeDataString(vacancyReference));
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


            if (customHeaders != null)
            {
                foreach(var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 200 && (int)_statusCode != 400 && (int)_statusCode != 404)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<TraineeshipVacancy>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int)_statusCode == 400)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<BadRequestContent>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// SearchApprenticeshipVacancies
        /// </summary>
        /// <remarks>
        /// The apprenticeship search operation retrieves live apprenticeship vacancies
        /// based on search criteria specified
        /// in the request parameters.
        ///
        /// Search criteria can be used to:
        ///
        /// - Search by framework LARS code(s)
        /// - Search by standard LARS code(s)
        /// - Search by framework or standard LARS code(s)
        /// - Search by location (geopoint) and radius
        /// - Search for recently posted vacancies
        /// - Search for nationwide vacancies
        ///
        /// #### Data paging ####
        ///
        /// Search results are returned in pages of data.
        /// If not specified then the default page size is 100 vacancies.
        /// If the search yields more data than can be included in a single page then
        /// additional pages can be requested by
        /// specifying a specific page number in the request. eg. pageNumber=2
        ///
        /// #### Examples ####
        ///
        /// To search for vacancies with standard code 94:
        ///
        /// ```
        /// /apprenticeships/search?standardLarsCodes=94
        /// ```
        ///
        /// Multiple standard codes can be specified by using a comma delimited list of
        /// standard codes.
        /// To search for vacancies with either standard code 94 or 95:
        ///
        /// ```
        /// /apprenticeships/search?standardLarsCodes=94,95
        /// ```
        ///
        /// To search for vacancies that went live within the last 2 days:
        ///
        /// ```
        /// /apprenticeships/search?postedInLastNumberOfDays=2
        /// ```
        ///
        /// To search for vacancies that went live today (0 days ago):
        ///
        /// ```
        /// /apprenticeships/search?postedInLastNumberOfDays=0
        /// ```
        ///
        /// To search for nationwide vacancies:
        ///
        /// ```
        /// /apprenticeships/search?nationwideOnly=true
        /// ```
        ///
        /// #### Combining parameters ####
        ///
        /// Multiple parameters can be added to the query string to refine the search.
        /// Note that when specifying both framework and standard codes, the results
        /// will include vacancies with matching
        /// framework or standard codes.
        ///
        /// #### Sorting results ####
        ///
        /// The results will be ordered by the following rules by default:
        /// - If searching by geo-location then the results are sorted by distance
        /// (closest first).
        /// - If searching by anything other than geo-location then the results are
        /// sorted by age (posted date) (newest first).
        ///
        /// The default sorting rules can be overriden by using the `SortBy` query
        /// parameter.
        /// SortBy can be set to "Age", "Distance" or "ExpectedStartDate".
        /// Whereas sorting by "Age" will return newest vacancies first, sorting by
        /// "ExpectedStartDate" will return vacancies that have earliest start date
        /// first.
        /// Beware that it is invalid to sort by distance if you have not searched by
        /// geo-location.
        ///
        /// #### Error codes ####
        ///
        /// The following error codes may be returned when calling this operation if
        /// any of the search criteria values
        /// specified fail validation:
        ///
        /// | Error code  | Explanation
        /// |
        /// | ----------- |
        /// --------------------------------------------------------------------------------
        /// |
        /// | 30100       | Search parameters not specified or insufficient
        /// |
        /// | 30101       | Invalid Standard Code
        /// |
        /// | 30102       | Invalid Framework code
        /// |
        /// | 30103       | Invalid Page size
        /// |
        /// | 30104       | Invalid Page number
        /// |
        /// | 30105       | Invalid Posted in last number of days
        /// |
        /// | 30106       | Invalid Latitude
        /// |
        /// | 30107       | Invalid Longitude
        /// |
        /// | 30108       | Invalid Distance in miles
        /// |
        /// | 30109       | Invalid NationwideOnly
        /// |
        /// | 30110       | Invalid SortBy
        /// |
        /// </remarks>
        /// <param name='standardLarsCodes'>
        /// </param>
        /// <param name='frameworkLarsCodes'>
        /// </param>
        /// <param name='pageSize'>
        /// Format - int32.
        /// </param>
        /// <param name='pageNumber'>
        /// Format - int32.
        /// </param>
        /// <param name='postedInLastNumberOfDays'>
        /// Format - int32.
        /// </param>
        /// <param name='nationwideOnly'>
        /// </param>
        /// <param name='latitude'>
        /// Format - double.
        /// </param>
        /// <param name='longitude'>
        /// Format - double.
        /// </param>
        /// <param name='distanceInMiles'>
        /// Format - int32.
        /// </param>
        /// <param name='sortBy'>
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> SearchApprenticeshipVacanciesAsync(string standardLarsCodes = default(string), string frameworkLarsCodes = default(string), int? pageSize = default(int?), int? pageNumber = default(int?), int? postedInLastNumberOfDays = default(int?), bool? nationwideOnly = default(bool?), double? latitude = default(double?), double? longitude = default(double?), int? distanceInMiles = default(int?), string sortBy = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("standardLarsCodes", standardLarsCodes);
                tracingParameters.Add("frameworkLarsCodes", frameworkLarsCodes);
                tracingParameters.Add("pageSize", pageSize);
                tracingParameters.Add("pageNumber", pageNumber);
                tracingParameters.Add("postedInLastNumberOfDays", postedInLastNumberOfDays);
                tracingParameters.Add("nationwideOnly", nationwideOnly);
                tracingParameters.Add("latitude", latitude);
                tracingParameters.Add("longitude", longitude);
                tracingParameters.Add("distanceInMiles", distanceInMiles);
                tracingParameters.Add("sortBy", sortBy);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "FivebFivefZeroNineFiveeaSevenNinedNinedOneZeroFivecEightEightdfSixb", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "v1/apprenticeships/search").ToString();
            List<string> _queryParameters = new List<string>();
            if (standardLarsCodes != null)
            {
                _queryParameters.Add(string.Format("standardLarsCodes={0}", System.Uri.EscapeDataString(standardLarsCodes)));
            }
            if (frameworkLarsCodes != null)
            {
                _queryParameters.Add(string.Format("frameworkLarsCodes={0}", System.Uri.EscapeDataString(frameworkLarsCodes)));
            }
            if (pageSize != null)
            {
                _queryParameters.Add(string.Format("pageSize={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(pageSize, SerializationSettings).Trim('"'))));
            }
            if (pageNumber != null)
            {
                _queryParameters.Add(string.Format("pageNumber={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(pageNumber, SerializationSettings).Trim('"'))));
            }
            if (postedInLastNumberOfDays != null)
            {
                _queryParameters.Add(string.Format("postedInLastNumberOfDays={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(postedInLastNumberOfDays, SerializationSettings).Trim('"'))));
            }
            if (nationwideOnly != null)
            {
                _queryParameters.Add(string.Format("nationwideOnly={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(nationwideOnly, SerializationSettings).Trim('"'))));
            }
            if (latitude != null)
            {
                _queryParameters.Add(string.Format("latitude={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(latitude, SerializationSettings).Trim('"'))));
            }
            if (longitude != null)
            {
                _queryParameters.Add(string.Format("longitude={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(longitude, SerializationSettings).Trim('"'))));
            }
            if (distanceInMiles != null)
            {
                _queryParameters.Add(string.Format("distanceInMiles={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(distanceInMiles, SerializationSettings).Trim('"'))));
            }
            if (sortBy != null)
            {
                _queryParameters.Add(string.Format("sortBy={0}", System.Uri.EscapeDataString(sortBy)));
            }
            if (_queryParameters.Count > 0)
            {
                _url += "?" + string.Join("&", _queryParameters);
            }
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


            if (customHeaders != null)
            {
                foreach(var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 200 && (int)_statusCode != 400)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = VacancySearchResults.FromJson(_responseContent);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int)_statusCode == 400)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<BadRequestContent>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }
        public async Task<HttpOperationResponse<object>> SearchApprenticeshipVacanciesByLocationAsync(double latitude, double longitude, int pageNumber, int pageSize, int distanceInMiles)
        {

            var result = SearchApprenticeshipVacanciesAsync(distanceInMiles: distanceInMiles, latitude: latitude, longitude: longitude, pageNumber: pageNumber,
                pageSize: pageSize).Result;

            return result;
        }

        public async Task<HttpOperationResponse<object>> SearchApprenticeshipVacanciesByLocationAsync(double latitude, double longitude, int pageNumber, int pageSize, int distanceInMiles, string standardLardCode)
        {

            var result = SearchApprenticeshipVacanciesAsync(distanceInMiles: distanceInMiles,latitude: latitude, longitude: longitude, pageNumber: pageNumber,
                pageSize: pageSize, standardLarsCodes:standardLardCode).Result;

            return result;
        }
    }
}
// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace VacanciesApi
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for LivevacanciesAPI.
    /// </summary>
    public static partial class LivevacanciesAPIExtensions
    {
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='vacancyReference'>
            /// </param>
            public static object GetApprenticeshipVacancy(this ILivevacanciesAPI operations, string vacancyReference)
            {
                return operations.GetApprenticeshipVacancyAsync(vacancyReference).GetAwaiter().GetResult();
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='vacancyReference'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetApprenticeshipVacancyAsync(this ILivevacanciesAPI operations, string vacancyReference, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetApprenticeshipVacancyAsync(vacancyReference, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='vacancyReference'>
            /// </param>
            public static object GetTraineeshipVacancy(this ILivevacanciesAPI operations, string vacancyReference)
            {
                return operations.GetTraineeshipVacancyAsync(vacancyReference).GetAwaiter().GetResult();
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='vacancyReference'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetTraineeshipVacancyAsync(this ILivevacanciesAPI operations, string vacancyReference, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetTraineeshipVacancyAsync(vacancyReference, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
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
            public static object SearchApprenticeshipVacancies(this ILivevacanciesAPI operations, string standardLarsCodes = default(string), string frameworkLarsCodes = default(string), int? pageSize = default(int?), int? pageNumber = default(int?), int? postedInLastNumberOfDays = default(int?), bool? nationwideOnly = default(bool?), double? latitude = default(double?), double? longitude = default(double?), int? distanceInMiles = default(int?), string sortBy = default(string))
            {
                return operations.SearchApprenticeshipVacanciesAsync(standardLarsCodes, frameworkLarsCodes, pageSize, pageNumber, postedInLastNumberOfDays, nationwideOnly, latitude, longitude, distanceInMiles, sortBy).GetAwaiter().GetResult();
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
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
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
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SearchApprenticeshipVacanciesAsync(this ILivevacanciesAPI operations, string standardLarsCodes = default(string), string frameworkLarsCodes = default(string), int? pageSize = default(int?), int? pageNumber = default(int?), int? postedInLastNumberOfDays = default(int?), bool? nationwideOnly = default(bool?), double? latitude = default(double?), double? longitude = default(double?), int? distanceInMiles = default(int?), string sortBy = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SearchApprenticeshipVacanciesAsync(standardLarsCodes, frameworkLarsCodes, pageSize, pageNumber, postedInLastNumberOfDays, nationwideOnly, latitude, longitude, distanceInMiles, sortBy, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}

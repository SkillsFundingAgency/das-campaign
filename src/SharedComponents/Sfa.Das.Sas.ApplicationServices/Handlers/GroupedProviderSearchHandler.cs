using System.Threading;
using Sfa.Das.Sas.ApplicationServices.Exceptions;
using Sfa.Das.Sas.ApplicationServices.Services;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Domain.Model;
    using FluentValidation;
    using MediatR;
    using Models;
    using Queries;
    using Responses;
    using Settings;
    using Validators;

    public sealed class GroupedProviderSearchHandler : IRequestHandler<GroupedProviderSearchQuery, GroupedProviderSearchResponse>
    {
        private readonly ILog _logger;
        private readonly IProviderSearchService _searchService;
        private readonly IPaginationSettings _paginationSettings;
        private readonly IPostcodeService _postcodeIoService;
        private readonly IValidator<GroupedProviderSearchQuery> _validator;

        private readonly Dictionary<string, ProviderSearchResponseCodes> _searchResponseCodes =
            new Dictionary<string, ProviderSearchResponseCodes>
                {
                  { LocationLookupResponse.WrongPostcode, ProviderSearchResponseCodes.PostCodeInvalidFormat },
                  { LocationLookupResponse.ServerError, ProviderSearchResponseCodes.LocationServiceUnavailable },
                  { LocationLookupResponse.ApprenticeshipNotFound, ProviderSearchResponseCodes.ApprenticeshipNotFound },
                  { ServerLookupResponse.InternalServerError, ProviderSearchResponseCodes.ServerError },
                  { LocationLookupResponse.Ok, ProviderSearchResponseCodes.Success },
                  { string.Empty, ProviderSearchResponseCodes.Success }
                };

        public GroupedProviderSearchHandler(
            IValidator<GroupedProviderSearchQuery> validator,
            IProviderSearchService searchService,
            IPaginationSettings paginationSettings,
            IPostcodeService postcodeIoService,
            ILog logger)
        {
            _validator = validator;
            _searchService = searchService;
            _paginationSettings = paginationSettings;
            _postcodeIoService = postcodeIoService;
            _logger = logger;
        }

        public async Task<GroupedProviderSearchResponse> Handle(GroupedProviderSearchQuery message, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(message);

            if (!result.IsValid)
            {
                var response = new GroupedProviderSearchResponse { Success = false };

                if (result.Errors.Any(x => x.ErrorCode == ValidationCodes.InvalidId))
                {
                    response.StatusCode = ProviderSearchResponseCodes.InvalidApprenticeshipId;
                }

                if (result.Errors.Any(x => x.ErrorCode == ValidationCodes.InvalidPostcode))
                {
                    response.StatusCode = ProviderSearchResponseCodes.PostCodeInvalidFormat;
                }

                return response;
            }

            var postcodeStatus = await GetPostcodeStatus(message.PostCode);

            switch (postcodeStatus)
            {
                case ProviderSearchResponseCodes.WalesPostcode:
                case ProviderSearchResponseCodes.ScotlandPostcode:
                case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                case ProviderSearchResponseCodes.PostCodeTerminated:
                case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                    return new GroupedProviderSearchResponse
                    {
                        Success = false,
                        StatusCode = postcodeStatus
                    };
                default:
                    message.Page = message.Page <= 0 ? 1 : message.Page;

                    return await PerformSearch(message);
            }
        }

        private async Task<ProviderSearchResponseCodes> GetPostcodeStatus(string postcode)
        {
            var status = await _postcodeIoService.GetPostcodeStatus(postcode);
            switch (status)
            {
                case "Wales":
                    return ProviderSearchResponseCodes.WalesPostcode;
                case "Scotland":
                    return ProviderSearchResponseCodes.ScotlandPostcode;
                case "Northern Ireland":
                    return ProviderSearchResponseCodes.NorthernIrelandPostcode;
                case "Terminated":
                    return ProviderSearchResponseCodes.PostCodeTerminated;
                case "Error":
                    return ProviderSearchResponseCodes.PostCodeInvalidFormat;
            }

            return ProviderSearchResponseCodes.Success;
        }

        private async Task<GroupedProviderSearchResponse> PerformSearch(GroupedProviderSearchQuery message)
        {
            var pageNumber = message.Page <= 0 ? 1 : message.Page;

            var hasNonLevyContract = message.IsLevyPayingEmployer == false;

            var searchResults = await _searchService.SearchProvidersWithGroupedResults(message.ApprenticeshipId, message.PostCode, new Pagination { Page = pageNumber, Take = message.Take }, hasNonLevyContract, message.NationalProvidersOnly);

            if (searchResults.TotalResults > 0 && !searchResults.Hits.Any())
            {
                var take = _paginationSettings.DefaultResultsAmount;
                var lastPage = take > 0 ? (int)System.Math.Ceiling((double)searchResults.TotalResults / take) : 1;
                return new GroupedProviderSearchResponse { StatusCode = ProviderSearchResponseCodes.PageNumberOutOfUpperBound, CurrentPage = lastPage };
            }

            var providerSearchResponse = new GroupedProviderSearchResponse()
            {
                Success = true,
                CurrentPage = pageNumber,
                Results = searchResults,
                SearchTerms = message.Keywords,
                ShowOnlyNationalProviders = message.NationalProvidersOnly,
                ShowAllProviders = message.ShowAll,
                StatusCode = GetResponseCode(searchResults.ResponseCode)
            };

            return providerSearchResponse;
        }

        private ProviderSearchResponseCodes GetResponseCode(string standardResponseCode)
        {
            return _searchResponseCodes[standardResponseCode ?? string.Empty];
        }
    }
}
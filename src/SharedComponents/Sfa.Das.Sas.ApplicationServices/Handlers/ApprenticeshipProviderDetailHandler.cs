using System;
using System.Threading;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    using System.Linq;
    using Core.Domain.Model;
    using Core.Domain.Repositories;
    using Core.Domain.Services;
    using FluentValidation;
    using MediatR;
    using Queries;
    using Responses;
    using SFA.DAS.Apprenticeships.Api.Types;
    using SFA.DAS.NLog.Logger;
    using Validators;

    public sealed class ApprenticeshipProviderDetailHandler : IRequestHandler<ApprenticeshipProviderDetailQuery, ApprenticeshipProviderDetailResponse>
    {
        private readonly AbstractValidator<ApprenticeshipProviderDetailQuery> _validator;
        private readonly IApprenticeshipProviderRepository _apprenticeshipProviderRepository;
        private readonly ILog _logger;
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;

        public ApprenticeshipProviderDetailHandler(
            AbstractValidator<ApprenticeshipProviderDetailQuery> validator,
            IApprenticeshipProviderRepository apprenticeshipProviderRepository,
            IGetStandards getStandards,
            IGetFrameworks getFrameworks,
            ILog logger)
        {
            _validator = validator;
            _apprenticeshipProviderRepository = apprenticeshipProviderRepository;
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _logger = logger;
        }

        public async Task<ApprenticeshipProviderDetailResponse> Handle(ApprenticeshipProviderDetailQuery message, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(message);

            if (result.Errors.Any(x => x.ErrorCode == ValidationCodes.InvalidInput))
            {
                return new ApprenticeshipProviderDetailResponse { StatusCode = ApprenticeshipProviderDetailResponse.ResponseCodes.InvalidInput };
            }

            if (!string.IsNullOrEmpty(message.ApprenticeshipId))
            {
                switch (message.ApprenticeshipType)
                {
                    case ApprenticeshipType.Framework:
                        message.FrameworkId = message.ApprenticeshipId;
                        break;
                    case ApprenticeshipType.Standard:
                        message.StandardCode = message.ApprenticeshipId;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (result.IsValid && !string.IsNullOrEmpty(message.StandardCode))
            {
                return GetStandard(message);
            }

            if (result.IsValid && !string.IsNullOrEmpty(message.FrameworkId))
            {
                return GetFramework(message);
            }

            return new ApprenticeshipProviderDetailResponse { StatusCode = ApprenticeshipProviderDetailResponse.ResponseCodes.ApprenticeshipProviderNotFound };
        }

        private ApprenticeshipProviderDetailResponse GetStandard(ApprenticeshipProviderDetailQuery message)
        {
            var model = _apprenticeshipProviderRepository.GetCourseByStandardCode(
                message.UkPrn,
                message.LocationId,
                message.StandardCode);

            var apprenticeshipData = _getStandards.GetStandardById(message.StandardCode);

            var response = CreateResponse(model, apprenticeshipData, ApprenticeshipTrainingType.Standard);

            if (apprenticeshipData != null)
            {
                response.RegulatedApprenticeship = apprenticeshipData.RegulatedStandard;
            }

            return response;
        }

        private ApprenticeshipProviderDetailResponse GetFramework(ApprenticeshipProviderDetailQuery message)
        {
            var model = _apprenticeshipProviderRepository.GetCourseByFrameworkId(
                message.UkPrn,
                message.LocationId,
                message.FrameworkId);

            var apprenticeshipProduct = _getFrameworks.GetFrameworkById(message.FrameworkId);

            var response = CreateResponse(model, apprenticeshipProduct, ApprenticeshipTrainingType.Framework);
            response.RegulatedApprenticeship = false;
            return response;
        }

        private ApprenticeshipProviderDetailResponse CreateResponse(ApprenticeshipDetails model, IApprenticeshipProduct apprenticeshipProduct, ApprenticeshipTrainingType apprenticeshipProductType)
        {
            if (model == null || apprenticeshipProduct == null)
            {
                return new ApprenticeshipProviderDetailResponse
                {
                    StatusCode = ApprenticeshipProviderDetailResponse.ResponseCodes.ApprenticeshipProviderNotFound
                };
            }

            var response = new ApprenticeshipProviderDetailResponse
            {
                StatusCode = ApprenticeshipProviderDetailResponse.ResponseCodes.Success,
                ApprenticeshipDetails = model,
                ApprenticeshipType = apprenticeshipProductType,
                ApprenticeshipName = apprenticeshipProduct.Title,
                ApprenticeshipLevel = apprenticeshipProduct.Level.ToString()
            };

            return response;
        }
    }
}
using System;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Web.Services.MappingActions.Helpers;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class TrainingProviderDetailsViewModelMapper : ITrainingProviderDetailsViewModelMapper
    {
        readonly IFeedbackViewModelMapper _feedbackMapper;

        public TrainingProviderDetailsViewModelMapper(IFeedbackViewModelMapper feedbackMapper)
        {
            _feedbackMapper = feedbackMapper;
        }

        public TrainingProviderDetailsViewModel Map(ApprenticeshipProviderDetailResponse source, string apprenticeshipId)
        {
            var employerSatisfationMessage =
              (source.ApprenticeshipDetails.Product.EmployerSatisfaction > 0)
                  ? ProviderMappingHelper.GetPercentageText(source.ApprenticeshipDetails.Product.EmployerSatisfaction)
                  : ProviderMappingHelper.GetPercentageText(null);

            var learnerSatisfationMessage =
                (source.ApprenticeshipDetails.Product.LearnerSatisfaction > 0)
                    ? ProviderMappingHelper.GetPercentageText(source.ApprenticeshipDetails.Product.LearnerSatisfaction)
                    : ProviderMappingHelper.GetPercentageText(null);

            var item = new TrainingProviderDetailsViewModel()
            {
                ContactInformation = new ContactInformation()
                {
                    Email = source.ApprenticeshipDetails.Provider.ContactInformation.Email,
                    Phone = source.ApprenticeshipDetails.Provider.ContactInformation.Phone,
                    Website = source.ApprenticeshipDetails.Provider.ContactInformation.Website,
                    ContactUsUrl = source.ApprenticeshipDetails.Provider.ContactInformation.ContactUsUrl
                },
                CurrentlyNotStartingNewApprentices = source.ApprenticeshipDetails.Provider.CurrentlyNotStartingNewApprentices,
                EmployerSatisfaction = Convert.ToInt32(source.ApprenticeshipDetails.Product.EmployerSatisfaction ?? 0.00),
                EmployerSatisfactionMessage = employerSatisfationMessage,
                IsHigherEducationInstitute = source.ApprenticeshipDetails.Provider.IsHigherEducationInstitute,
                LearnerSatisfaction = Convert.ToInt32(source.ApprenticeshipDetails.Product.LearnerSatisfaction ?? 0.00),
                LearnerSatisfactionMessage = learnerSatisfationMessage,
                NationalProvider = source.ApprenticeshipDetails.Provider.NationalProvider,
                Ukprn = source.ApprenticeshipDetails.Provider.UkPrn,
                Name = source.ApprenticeshipDetails.Provider.Name,
                DeliveryModes = source.ApprenticeshipDetails.Product.DeliveryModes,
                MarketingInfo = source.ApprenticeshipDetails.Product.ProviderMarketingInfo,
                HasParentCompanyGuarantee = source.ApprenticeshipDetails.Provider.HasParentCompanyGuarantee,
                IsNewProvider = source.ApprenticeshipDetails.Provider.IsNew,
                IsLevyPayerOnly = source.ApprenticeshipDetails.Provider.IsLevyPayerOnly,
                AchievementRate = Convert.ToInt32(source.ApprenticeshipDetails.Product.AchievementRate),
                NationalAchievementRate = Convert.ToInt32(source.ApprenticeshipDetails.Product.NationalAchievementRate),
                LocationAddressLine = ProviderMappingHelper.GetCommaList(source.ApprenticeshipDetails.Location.LocationName, source.ApprenticeshipDetails.Location.Address.Address1, source.ApprenticeshipDetails.Location.Address.Address2, source.ApprenticeshipDetails.Location.Address.Town, source.ApprenticeshipDetails.Location.Address.County, source.ApprenticeshipDetails.Location.Address.Postcode),
                LegalName = source.ApprenticeshipDetails.Provider.LegalName,
                Feedback = _feedbackMapper.Map(source.ApprenticeshipDetails.Provider.ProviderFeedback),
                AboutApprenticeshipInfo = source.ApprenticeshipDetails.Product.Apprenticeship.ApprenticeshipMarketingInfo,
                Postcode = source.ApprenticeshipDetails.Location.Address.Postcode,
                Location = source.ApprenticeshipDetails.Location,
                ApprenticeshipId = apprenticeshipId
                
            };

            return item;
        }
    }
}

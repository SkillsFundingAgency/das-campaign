namespace Sfa.Das.Sas.ApplicationServices.Validators
{
    using FluentValidation;

    using Sfa.Das.Sas.ApplicationServices.Queries;

    public sealed class ApprenticeshipProviderDetailQueryValidator : AbstractValidator<ApprenticeshipProviderDetailQuery>
    {
        public ApprenticeshipProviderDetailQueryValidator(IValidation validation)
        {
            RuleFor(criteria => criteria.UkPrn).GreaterThan(0).WithErrorCode(ValidationCodes.InvalidInput);
            RuleFor(criteria => criteria.LocationId).GreaterThan(0).WithErrorCode(ValidationCodes.InvalidInput);
        }
    }
}
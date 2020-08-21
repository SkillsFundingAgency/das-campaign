using FluentValidation;
using Sfa.Das.Sas.ApplicationServices.Queries;

namespace Sfa.Das.Sas.ApplicationServices.Validators
{
    public sealed class GetClosestLocationsQueryValidator : AbstractValidator<GetClosestLocationsQuery>
    {
        public GetClosestLocationsQueryValidator(IValidation validation)
        {
            RuleFor(criteria => criteria.ApprenticeshipId).NotEmpty();
            RuleFor(criteria => criteria.Ukprn).GreaterThanOrEqualTo(10000000).LessThanOrEqualTo(999999999);
            RuleFor(criteria => criteria.PostCode).NotEmpty().Must(validation.ValidatePostcode);
        }
    }
}

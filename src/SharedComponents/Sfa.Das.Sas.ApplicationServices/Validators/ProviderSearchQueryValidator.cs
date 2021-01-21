using FluentValidation;
using Sfa.Das.Sas.ApplicationServices.Queries;

namespace Sfa.Das.Sas.ApplicationServices.Validators
{
    public sealed class ProviderSearchQueryValidator : AbstractValidator<ProviderSearchQuery>
    {
        public ProviderSearchQueryValidator(IValidation validation)
        {
            RuleFor(criteria => criteria.PostCode).NotEmpty().Must(validation.ValidatePostcode).WithErrorCode(ValidationCodes.InvalidPostcode);
        }
    }
}

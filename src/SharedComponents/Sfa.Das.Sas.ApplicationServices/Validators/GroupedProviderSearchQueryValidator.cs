using FluentValidation;
using Sfa.Das.Sas.ApplicationServices.Queries;

namespace Sfa.Das.Sas.ApplicationServices.Validators
{

    public sealed class GroupedProviderSearchQueryValidator : AbstractValidator<GroupedProviderSearchQuery>
    {
        public GroupedProviderSearchQueryValidator(IValidation validation)
        {
            RuleFor(criteria => criteria.PostCode).NotEmpty().Must(validation.ValidatePostcode).WithErrorCode(ValidationCodes.InvalidPostcode);
        }
    }
}

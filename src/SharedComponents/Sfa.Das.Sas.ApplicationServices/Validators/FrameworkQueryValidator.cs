namespace Sfa.Das.Sas.ApplicationServices.Validators
{
    using FluentValidation;

    using Sfa.Das.Sas.ApplicationServices.Queries;

    public sealed class FrameworkQueryValidator : AbstractValidator<GetFrameworkQuery>
    {
        public FrameworkQueryValidator(IValidation validation)
        {
            RuleFor(criteria => criteria.Id).NotEmpty().Must(validation.ValidateFrameowrkId).WithErrorCode(ValidationCodes.InvalidId);
        }
    }
}
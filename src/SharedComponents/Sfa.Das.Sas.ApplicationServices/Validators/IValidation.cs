namespace Sfa.Das.Sas.ApplicationServices.Validators
{
    public interface IValidation
    {
        bool ValidatePostcode(string postCode);

        bool ValidateFrameowrkId(string id);
    }
}
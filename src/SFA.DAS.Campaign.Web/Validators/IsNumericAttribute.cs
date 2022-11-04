using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.Campaign.Web.Validators
{
    public class IsNumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal val;
                var isNumeric = decimal.TryParse(value.ToString(), out val);

                if (!isNumeric)
                {
                    return new ValidationResult("Enter a number, like 1 or 2");
                }
            }
            return ValidationResult.Success;
        }
    }
}

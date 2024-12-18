using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SFA.DAS.Campaign.Web.Validators
{
    public class RolesValidationAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public RolesValidationAttribute(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Required check: Value must be provided
                ErrorMessage = "Enter how many roles you have available for this apprenticeship";
                return false;
            }

            string stringValue = value.ToString();

            // Whole number check: Value must be a whole number
            if (!Regex.IsMatch(stringValue, @"^\d+$"))
            {
                ErrorMessage = "Number of roles available for this apprenticeship must be a whole number";
                return false;
            }

            // Minimum value check: Value must be at least _minValue
            if (int.TryParse(stringValue, out int intValue) && intValue < _minValue)
            {
                ErrorMessage = $"Number of roles available for this apprenticeship must be {_minValue} or more";
                return false;
            }

            return true;
        }
    }
}
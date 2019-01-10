using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.Campaign.Models.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Results = new List<string>();
        }

        public void AddError(string paramName, ValidationFailure validationFailure)
        {
            var validationResultError = validationFailure == ValidationFailure.NotValid 
                ? $"The {paramName} field is not valid." : $"The {paramName} field is required.";

            Results.Add($"{paramName}|{validationResultError}");
        }

        public List<string> Results { get; set; }
        public bool IsValid => !Results.Any();
    }

    public enum ValidationFailure
    {
        NotPopulated = 0,
        NotValid = 1
    }
}

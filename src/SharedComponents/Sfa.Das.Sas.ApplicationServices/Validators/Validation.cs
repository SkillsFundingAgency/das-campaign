﻿using System.Text.RegularExpressions;

namespace Sfa.Das.Sas.ApplicationServices.Validators
{

    public class Validation : IValidation
    {
        public bool ValidatePostcode(string postCode)
        {
            var match = Regex.Match(postCode ?? string.Empty, @"^[A-Z]{1,2}[0-9][A-Z0-9]?\s?[0-9][ABD-HJLNP-UW-Z]{2}$", RegexOptions.IgnoreCase);

            return match.Success;
        }

        public bool ValidateFrameowrkId(string id)
        {
            // ToDo: "Remove match on only \d when there is no risk of having that ID formt. CF"
            return Regex.IsMatch(id, @"^\d+-\d+-\d+$") || Regex.IsMatch(id, @"^\d+$");
        }
    }
}
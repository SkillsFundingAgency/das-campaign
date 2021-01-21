using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sfa.Das.Sas.Shared.Components.Extensions
{
    public static class StringExtensions
    {

        public static string Pluralize(this string singularForm, int howMany)
        {
            return singularForm.Pluralize(howMany, singularForm + "s");
        }

        public static string Pluralize(this string singularForm, int howMany, string pluralForm)
        {
            return howMany == 1 ? singularForm : pluralForm;
        }
    }
}
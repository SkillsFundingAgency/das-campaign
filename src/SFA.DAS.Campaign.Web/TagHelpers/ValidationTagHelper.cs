using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.Campaign.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "validation-row-status")]
    public class ValidationTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        public string PropertyName { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PropertyIsInvalid())
                output.Attributes.Add("class", "govuk-form-group--error");
        }

        bool PropertyIsInvalid()
        {
            return ViewContext?.ModelState[PropertyName]?.ValidationState == ModelValidationState.Invalid;
        }
    }
}
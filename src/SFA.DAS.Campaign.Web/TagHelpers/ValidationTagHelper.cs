using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace SFA.DAS.Campaign.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "highlight-error-for")]
    public class ValidationTagHelper : TagHelper
    {
        private const string HighlightErrorForAttributeName = "highlight-error-for";
        private const string ErrorCssClass = "govuk-form-group--error";

        [HtmlAttributeName(HighlightErrorForAttributeName)]
        public ModelExpression Property { get; set; }

        [HtmlAttributeName(ErrorCssClass)]
        public string CssClass { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!ViewContext.ModelState.ContainsKey(Property.Name)) return;

            var modelState = ViewContext.ModelState[Property.Name];

            if (modelState.Errors.Count == 0) return;
            output.AddClass(ErrorCssClass, HtmlEncoder.Default);
        }
    }
}
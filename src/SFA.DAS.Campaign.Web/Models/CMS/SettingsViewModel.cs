using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Errors;
using Microsoft.AspNetCore.Mvc.Localization;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    public class SettingsViewModel
    {
        /// <summary>
        /// The current options the application is using.
        /// </summary>
        public ContentfulOptions Options { get; set; }

        /// <summary>
        /// The options displayed in the view, editeable by the user.
        /// </summary>
        public SelectedOptions AppOptions { get; set; }

        /// <summary>
        /// The name of the currently connected space.
        /// </summary>
        public string SpaceName { get; set; }

        public bool IsUsingCustomCredentials { get; set; }
    }
    /// <summary>
    /// Class encapsulating the options a user can set through the /settings view.
    /// </summary>
    public class SelectedOptions : IValidatableObject
    {
        /// <summary>
        /// The selected space id.
        /// </summary>
        public string SpaceId { get; set; }

        /// <summary>
        /// The delivery API access token for the space.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The preview API access token for the space.
        /// </summary>
        public string PreviewToken { get; set; }

        /// <summary>
        /// Whether or not to use the preview API.
        /// </summary>
        public bool UsePreviewApi { get; set; }

        /// <summary>
        /// Whether or not to enable editorial features.
        /// </summary>
        public bool EnableEditorialFeatures { get; set; }

        /// <summary>
        /// Validation logic for the options.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>An IEnumerable of ValidationResult.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localizer = validationContext.GetService(typeof(IViewLocalizer)) as IViewLocalizer;
            if (string.IsNullOrEmpty(SpaceId))
            {
                yield return new ValidationResult(localizer["fieldIsRequiredLabel"].Value, new[] { nameof(SpaceId) });
            }
            if (string.IsNullOrEmpty(AccessToken))
            {
                yield return new ValidationResult(localizer["fieldIsRequiredLabel"].Value, new[] { nameof(AccessToken) });
            }
            if (string.IsNullOrEmpty(PreviewToken))
            {
                yield return new ValidationResult(localizer["fieldIsRequiredLabel"].Value, new[] { nameof(PreviewToken) });
            }

            if (!string.IsNullOrEmpty(SpaceId) && !string.IsNullOrEmpty(AccessToken) && !string.IsNullOrEmpty(PreviewToken))
            {
                // We got all required fields. Make a test call to each API to verify.
                var httpClient = validationContext.GetService(typeof(HttpClient)) as HttpClient;

                var contentfulClient = new ContentfulClient(httpClient, AccessToken, PreviewToken, SpaceId);

                foreach (var validationResult in MakeTestCalls(httpClient, contentfulClient, localizer))
                {
                    yield return validationResult;
                }
            }
        }

        private IEnumerable<ValidationResult> MakeTestCalls(HttpClient httpClient, IContentfulClient contentfulClient, IViewLocalizer localizer)
        {
            ValidationResult validationResult = null;

            try
            {
                var space = contentfulClient.GetSpace().Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle((ce) => {
                    if (ce is ContentfulException)
                    {
                        if ((ce as ContentfulException).StatusCode == 401)
                        {
                            validationResult = new ValidationResult(localizer["deliveryKeyInvalidLabel"].Value, new[] { nameof(AccessToken) });
                        }
                        else if ((ce as ContentfulException).StatusCode == 404)
                        {
                            validationResult = new ValidationResult(localizer["spaceOrTokenInvalid"].Value, new[] { nameof(SpaceId) });
                        }
                        else
                        {
                            validationResult = new ValidationResult($"{localizer["somethingWentWrongLabel"].Value}: {ce.Message}", new[] { nameof(AccessToken) });
                        }
                        return true;
                    }
                    return false;
                });

            }

            if (validationResult != null)
            {
                yield return validationResult;
            }

            contentfulClient = new ContentfulClient(httpClient, AccessToken, PreviewToken, SpaceId, true);

            try
            {
                var space = contentfulClient.GetSpace().Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle((ce) => {
                    if (ce is ContentfulException)
                    {

                        if ((ce as ContentfulException).StatusCode == 401)
                        {
                            validationResult = new ValidationResult(localizer["previewKeyInvalidLabel"].Value, new[] { nameof(PreviewToken) });
                        }
                        else if ((ce as ContentfulException).StatusCode == 404)
                        {
                            validationResult = new ValidationResult(localizer["spaceOrTokenInvalid"].Value, new[] { nameof(SpaceId) });
                        }
                        else
                        {
                            validationResult = new ValidationResult($"{localizer["somethingWentWrongLabel"].Value}: {ce.Message}", new[] { nameof(PreviewToken) });
                        }
                        return true;
                    }
                    return false;
                });
            }

            if (validationResult != null)
            {
                yield return validationResult;
            }
        }
    }
}

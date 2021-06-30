using System;
using System.Collections.Generic;
using System.Text;
using Polly.Caching;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class ImageControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var image = new Image
            {
                Title = control.EmbeddedResource.Title,
                Url = control.EmbeddedResource.Url,
                Description = control.EmbeddedResource.Description
            };

            return image;
        }

        public bool IsValid(Item control)
        {
            if (control.Type.StartsWith("embedded-asset-block", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}

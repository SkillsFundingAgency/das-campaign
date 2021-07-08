using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class SiteMapJsonConverter : JsonConverter, ICmsPageConverter
    {
        public SiteMapJsonConverter()
        {
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var jObject = JObject.Load(reader);
            var cmsContent = new PageRoot();
            serializer.Populate(jObject.CreateReader(), cmsContent);

            return PopulatePageModel(cmsContent);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Page<SiteMap>).IsAssignableFrom(objectType);
        }

        private Page<SiteMap> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<SiteMap>
            {
                Content = new SiteMap
                {
                    Urls = AddSiteMapUrls(cmsContent)
                }
            };

            return pageModel;
        }

        private List<SiteMapUrl> AddSiteMapUrls(PageRoot cmsContent)
        {
            return cmsContent.Map.MainContent.Pages.Select(page => new SiteMapUrl {Title = page.Title, Hub = page.Hub, PageType = page.PageType, Slug = page.Slug}).ToList();
        }
    }
}

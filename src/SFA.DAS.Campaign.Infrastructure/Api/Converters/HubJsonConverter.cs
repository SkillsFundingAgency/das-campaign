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
    public class HubJsonConverter : JsonConverter, ICmsPageConverter
    {
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
            return typeof(Page<Hub>).IsAssignableFrom(objectType);
        }

        private Page<Hub> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<Hub>
            {
                Slug = cmsContent.Hub.PageAttributes.Slug,
                HubType = Enum.Parse<HubType>(cmsContent.Hub.PageAttributes.HubType),
                Title = cmsContent.Hub.PageAttributes.Title,
                MetaContent = new MetaContent
                {
                    MetaDescription = cmsContent.Hub.PageAttributes?.MetaDescription,
                    PageTitle = cmsContent.Hub.PageAttributes.Title
                },
                Content = new Hub
                {
                    Summary = cmsContent.Hub.PageAttributes.Summary
                }
            };

            pageModel.PopulateMenuModel(cmsContent.Hub.MenuContent);

            AddHeaderImage(cmsContent, pageModel);
            AddCards(cmsContent, pageModel);
            
            return pageModel;
        }

        private void AddHeaderImage(PageRoot cmsContent, Page<Hub> model)
        {
            model.Content.HeaderImage = new Image
            {
                Description = cmsContent.Hub.MainContent?.HeaderImage?.EmbeddedResource.Description,
                Title = cmsContent.Hub.MainContent?.HeaderImage?.EmbeddedResource.Title,
                Url = cmsContent.Hub.MainContent?.HeaderImage?.EmbeddedResource.Url
            };
        }
        private static void AddCards(PageRoot cmsContent, Page<Hub> model)
        {
            if (cmsContent.Hub.MainContent.Cards == null || !cmsContent.Hub.MainContent.Cards.Any())
            {
                return;
            }

            model.Content.Cards = cmsContent.Hub.MainContent.Cards;
        }
    }
}

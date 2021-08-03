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
    public class LandingPageJsonConverter : JsonConverter, ICmsPageConverter
    {
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public LandingPageJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
        {
            _controlAbstractFactory = controlAbstractFactory;
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
            return typeof(Page<LandingPage>).IsAssignableFrom(objectType);
        }

        private Page<LandingPage> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<LandingPage>
            {
                Slug = cmsContent.LandingPage.PageAttributes.Slug,
                HubType = Enum.Parse<HubType>(cmsContent.LandingPage.PageAttributes.HubType),
                Title = cmsContent.LandingPage.PageAttributes.Title,
                MetaContent = new MetaContent
                {
                    MetaDescription = cmsContent.LandingPage.PageAttributes.MetaDescription,
                    PageTitle = cmsContent.LandingPage.PageAttributes.Title
                },
                Content = new LandingPage()
                {
                    Summary = cmsContent.LandingPage.PageAttributes.Summary
                }
            };

            AddHeaderImage(cmsContent, pageModel);
            AddCards(cmsContent, pageModel);
            pageModel.PopulateMenuModel(cmsContent.LandingPage.MenuContent);
            pageModel.AddBannerContent(cmsContent, _controlAbstractFactory, cmsContent.LandingPage.BannerModels);
            return pageModel;
        }

        private void AddHeaderImage(PageRoot cmsContent, Page<LandingPage> model)
        {
            model.Content.HeaderImage = new Image
            {
                Description = cmsContent.LandingPage.MainContent?.HeaderImage?.EmbeddedResource.Description,
                Title = cmsContent.LandingPage.MainContent?.HeaderImage?.EmbeddedResource.Title,
                Url = cmsContent.LandingPage.MainContent?.HeaderImage?.EmbeddedResource.Url
            };
        }
        private static void AddCards(PageRoot cmsContent, Page<LandingPage> model)
        {
            if (cmsContent.LandingPage.MainContent.Cards == null || !cmsContent.LandingPage.MainContent.Cards.Any())
            {
                return;
            }

            model.Content.Cards = cmsContent.LandingPage.MainContent.Cards;
        }
    }
}

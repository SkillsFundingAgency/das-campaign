using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using System;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class PanelJsonConverter : JsonConverter, ICmsPageConverter
    {
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public PanelJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
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
            return typeof(Panel).IsAssignableFrom(objectType);
        }

        private Panel PopulatePageModel(PageRoot cmsContent)
        {

            var pageModel = new Panel
            {
                Slug = cmsContent.Panel.PageAttributes.Slug,
                Title = cmsContent.Panel.PageAttributes.Title
            };

            AddPanelButton(cmsContent, pageModel);
            AddPanelImage(cmsContent, pageModel);
            AddPageContent(cmsContent, pageModel);

            return pageModel;
        }

        private void AddPanelButton(PageRoot cmsContent, Panel model)
        {
            var panelButton = cmsContent.Panel.MainContent.Button;

            if (panelButton == null || string.IsNullOrWhiteSpace(panelButton.Url))
            {
                return;
            }

            model.Button = new Button
            {
                Title = panelButton.Title,
                Url = panelButton.Url,
                Styles = panelButton.Styles
            };
        }

        private void AddPanelImage(PageRoot cmsContent, Panel model)
        {
            var panelImage = cmsContent.Panel.MainContent.Image;

            if (panelImage == null || string.IsNullOrWhiteSpace(panelImage.Url))
            {
                return;
            }

            model.MainImage = new Image
            {
                Title = panelImage.Title,
                Description = panelImage.Description,
                Url = panelImage.Url
            };
        }

        private void AddPageContent(PageRoot cmsContent, Panel model)
        {
            var pageContent = new List<IHtmlControl>();

            foreach (var content in cmsContent.Panel.MainContent.Items)
            {
                var factory = _controlAbstractFactory.CreateControlFactoryFor(content);

                if (factory == null)
                {
                    continue;
                }

                pageContent.Add(factory.Create(content));
            }

            model.Content = pageContent;
        }
    }
}

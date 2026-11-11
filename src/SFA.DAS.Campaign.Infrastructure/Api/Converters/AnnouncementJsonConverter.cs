using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class AnnouncementJsonConverter : JsonConverter, ICmsPageConverter
    {
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;

        public AnnouncementJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
        {
            _controlAbstractFactory = controlAbstractFactory;
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
            var cmsContent = new AnnouncementRoot();
            serializer.Populate(jObject.CreateReader(), cmsContent);

            return PopulateModel(cmsContent);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Announcement).IsAssignableFrom(objectType);
        }

        private Announcement PopulateModel(AnnouncementRoot cmsContent)
        {
            var mainContent = cmsContent.Announcement?.MainContent;

            if (mainContent == null)
            {
                return null;
            }

            return new Announcement
            {
                Id = mainContent.Id,
                Headline = mainContent.Headline,
                Slug = mainContent.Slug,
                Intro = mainContent.Intro,
                PublishedDate = mainContent.PublishedDate,
                UpdatedDate = mainContent.UpdatedDate,
                Audience = mainContent.Audience,
                Category = mainContent.Category,
                Content = BuildControls(mainContent.Items),
                Sections = BuildSections(cmsContent.Announcement.Sections)
            };
        }

        private List<TabbedContent> BuildSections(List<ResponseTabbedContent> sections)
        {
            var result = new List<TabbedContent>();

            if (sections == null)
            {
                return result;
            }

            foreach (var section in sections)
            {
                result.Add(new TabbedContent
                {
                    TabName = section.TabName,
                    TabTitle = section.TabTitle,
                    FindTraineeShip = section.FindTraineeship,
                    Content = BuildControls(section.Content?.Items)
                });
            }

            return result;
        }

        private List<IHtmlControl> BuildControls(List<Item> items)
        {
            var controls = new List<IHtmlControl>();

            if (items == null)
            {
                return controls;
            }

            foreach (var item in items)
            {
                var factory = _controlAbstractFactory.CreateControlFactoryFor(item);

                if (factory == null)
                {
                    continue;
                }

                controls.Add(factory.Create(item));
            }

            return controls;
        }
    }
}

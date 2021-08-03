using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class BannerJsonConverter : JsonConverter, ICmsPageConverter
    {
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;

        public BannerJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
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
            var cmsContent = new PageRoot();
            serializer.Populate(jObject.CreateReader(), cmsContent);

            return PopulatePageModel(cmsContent);
        }

        public override bool CanConvert(Type objectType)
        {

            return typeof(Page<BannerContentType>).IsAssignableFrom(objectType);
        }

        private Page<BannerContentType> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<BannerContentType>();

            pageModel.AddBannerContent(_controlAbstractFactory, cmsContent.Banner);

            return pageModel;
        }
    }
}

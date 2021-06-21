using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ArticleJsonConverter : JsonConverter
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
            var cmsContent = new Root();
            serializer.Populate(jObject.CreateReader(), cmsContent);

            return PopulatePageModel(cmsContent);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Page<Article>).IsAssignableFrom(objectType);
        }

        private Page<Article> PopulatePageModel(Root cmsContent)
        {
            var pageModel = new Page<Article>();

            foreach (var content in cmsContent.MainContent.Items)
            {
                if (content.Type == "paragraph")
                {
                    Console.Write("");
                }
            }

            return pageModel;
        }
    }
}

public class PageAttributes
{
    public int PageType { get; set; }
    public string Title { get; set; }
    public string MetaDescription { get; set; }
    public string Slug { get; set; }
    public string HubType { get; set; }
    public string Summary { get; set; }
}

public class Item
{
    public Item()
    {
        Values = new List<string>();
        TableValue = new List<List<string>>();
    }
    public List<string> Values { get; set; }
    public string Type { get; set; }
    public List<List<string>> TableValue { get; set; }
}

public class MainContent
{
    public List<Item> Items { get; set; }
}

public class RelatedArticle
{
    public int PageType { get; set; }
    public string Title { get; set; }
    public string MetaDescription { get; set; }
    public string Slug { get; set; }
    public string HubType { get; set; }
    public string Summary { get; set; }
}

public class Root
{
    public PageAttributes PageAttributes { get; set; }
    public MainContent MainContent { get; set; }
    public List<RelatedArticle> RelatedArticles { get; set; }
}
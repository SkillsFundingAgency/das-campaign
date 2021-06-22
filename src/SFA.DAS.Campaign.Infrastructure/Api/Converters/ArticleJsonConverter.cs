using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ArticleJsonConverter : JsonConverter
    {
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;

        public ArticleJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
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
            return typeof(Page<Article>).IsAssignableFrom(objectType);
        }

        private Page<Article> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<Article>
            {
                Slug = cmsContent.Article.PageAttributes.Slug,
                HubType = Enum.Parse<HubType>(cmsContent.Article.PageAttributes.HubType),
                Title = cmsContent.Article.PageAttributes.Title,
                MetaContent = new MetaContent
                {
                    MetaDescription = cmsContent.Article.PageAttributes.MetaDescription,
                    PageTitle = cmsContent.Article.PageAttributes.Title
                }
            };

            AddRelatedArticles(cmsContent, pageModel);

            AddPageContent(cmsContent, pageModel);

            return pageModel;
        }

        private void AddPageContent(PageRoot cmsContent, Page<Article> model)
        {
            var pageContent = new List<IHtmlControl>();

            foreach (var content in cmsContent.Article.MainContent.Items)
            {
                var factory = _controlAbstractFactory.CreateControlFactoryFor(content);

                if (factory == null)
                {
                    continue;
                }

                pageContent.Add(factory.Create(content));
            }

            model.Content = new Article
            {
                PageControls = pageContent, Summary = cmsContent.Article.PageAttributes.Summary
            };
        }

        private static void AddRelatedArticles(PageRoot cmsContent, Page<Article> model)
        {
            if (cmsContent.Article.RelatedArticles == null || !cmsContent.Article.RelatedArticles.Any())
            {
                return;
            }

            var relatedPages = cmsContent.Article.RelatedArticles.Select(relatedArticle => new RelatedPage {Summary = relatedArticle.Summary, HubType = relatedArticle.HubType, Slug = relatedArticle.Slug}).ToList();
            model.RelatedPages = relatedPages;
        }
    }
}

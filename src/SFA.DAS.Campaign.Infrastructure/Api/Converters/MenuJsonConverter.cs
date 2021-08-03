﻿using System;
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
    public class MenuJsonConverter : JsonConverter, ICmsPageConverter
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
            return typeof(Page<Menu>).IsAssignableFrom(objectType);
        }

        private Page<Menu> PopulatePageModel(PageRoot cmsContent)
        {
            var pageModel = new Page<Menu>();
            pageModel.PopulateMenuModel(cmsContent);
            
            return pageModel;
        }
    }

    internal static class Extensions
    {
        internal static List<Url> AddSiteMapUrls(this List<SiteMapPage> page)
        {
            return page.Select(page => new Url { Title = page.Title, Hub = page.Hub, PageType = page.PageType, Slug = page.Slug }).ToList();
        }

        internal static void PopulateMenuModel<T>(this Page<T> page, PageRoot cmsContent) where T : IContentType
        {
            page.Menu = new Menu
            {
                Apprentices = cmsContent.Menu.MainContent.Apprentices.AddSiteMapUrls(),
                Employers = cmsContent.Menu.MainContent.Employers.AddSiteMapUrls(),
                Influencers = cmsContent.Menu.MainContent.Influencers.AddSiteMapUrls(),
                TopLevel = cmsContent.Menu.MainContent.TopLevel.AddSiteMapUrls()
            };
        }

        internal static void PopulateMenuModel<T>(this Page<T> page, MenuContent cmsContent) where T : IContentType
        {
            page.Menu = new Menu
            {
                Apprentices = cmsContent.Apprentices.AddSiteMapUrls(),
                Employers = cmsContent.Employers.AddSiteMapUrls(),
                Influencers = cmsContent.Influencers.AddSiteMapUrls(),
                TopLevel = cmsContent.TopLevel.AddSiteMapUrls()
            };
        }

        internal static void AddBannerContent<T>(this Page<T> model, PageRoot cmsContent, IHtmlControlAbstractFactory controlAbstractFactory, ResponseBanner cmsContentBanner) where T : IContentType
        {
            if (cmsContentBanner == null)
            {
                model.Banners = new List<Banner>();
                return;
            }

            var banners = new List<Banner>();
            foreach (var banner in cmsContentBanner.MainContent)
            {
                var bannerModel = new Banner
                {
                    AllowUserToHideTheBanner = banner.AllowUserToHideTheBanner,
                    BackgroundColour = banner.BackgroundColour,
                    ShowOnTheHomepageOnly = banner.ShowOnTheHomepageOnly,
                    Title = banner.Title,
                    Id = banner.Id
                };


                var pageContent = new List<IHtmlControl>();

                foreach (var content in banner.Items)
                {
                    var factory = controlAbstractFactory.CreateControlFactoryFor(content);

                    if (factory == null)
                    {
                        continue;
                    }

                    pageContent.Add(factory.Create(content));
                }

                bannerModel.Content = pageContent;
                banners.Add(bannerModel);
            }

            model.Banners = banners;
        }
    }
}

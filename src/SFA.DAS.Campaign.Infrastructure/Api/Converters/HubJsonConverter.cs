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
        private readonly IHtmlControlAbstractFactory _controlAbstractFactory;
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public HubJsonConverter(IHtmlControlAbstractFactory controlAbstractFactory)
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
            return typeof(Page<Hub>).IsAssignableFrom(objectType);
        }

        private Page<Hub> PopulatePageModel(PageRoot cmsContent)
        {
            if (!cmsContent.Hub.PageAttributes.HubType.TryGetHubType(out var hubType))
            {
                return null;
            }

            var pageModel = new Page<Hub>
            {
                Slug = cmsContent.Hub.PageAttributes.Slug,
                HubType = hubType,
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
            pageModel.AddBannerContent(_controlAbstractFactory, cmsContent.Hub.BannerModels);
            AddHeaderImage(cmsContent, pageModel);
            AddCards(cmsContent, pageModel);
            AddSections(cmsContent, pageModel);
            AddCarousel(cmsContent, pageModel);
            
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
            var mainContent = cmsContent.Hub.MainContent;

            if (mainContent == null)
            {
                return;
            }

            model.Content.CardsTitle = mainContent.CardsTitle;
            model.Content.Cards = MapCards(mainContent.Cards);

            model.Content.CardsTitle2 = mainContent.CardsTitle2;
            model.Content.Cards2 = MapCards(mainContent.Cards2);

            model.Content.CardsTitle3 = mainContent.CardsTitle3;
            model.Content.Cards3 = MapCards(mainContent.Cards3);
        }

        private static void AddCarousel(PageRoot cmsContent, Page<Hub> model)
        {
            var carousel = cmsContent.Hub.MainContent?.Carousel;

            if (carousel == null || !carousel.Any())
            {
                return;
            }

            model.Content.Carousel = carousel
                .Select(MapImage)
                .Where(image => image != null)
                .ToList();
        }

        private static void AddSections(PageRoot cmsContent, Page<Hub> model)
        {
            var sections = cmsContent.Hub.MainContent?.Sections;

            if (sections == null || !sections.Any())
            {
                return;
            }

            var hubType = model.HubType.ToString();

            model.Content.Sections = sections
                .Where(section => section != null)
                .Select(section => new HubSection
                {
                    SectionType = section.SectionType,
                    Heading = section.Heading,
                    Introduction = section.Introduction,
                    Image = MapImage(section.Image),
                    StepperLinks = MapSectionLinks(section.StepperLinks, hubType),
                    StandardLinks = MapSectionLinks(section.StandardLinks, hubType),
                    Statistics = MapStatistics(section.StatisticsSections),
                    CtaPanel = MapCtaPanel(section.CtaPanel)
                })
                .Where(section => section.HasContent)
                .ToList();
        }

        private static List<HubSectionLink> MapSectionLinks(List<ResponseHubSectionLink> links, string hubType)
        {
            if (links == null || !links.Any())
            {
                return new List<HubSectionLink>();
            }

            return links
                .Where(link => link != null)
                .Select(link => new HubSectionLink
                {
                    Title = link.Title,
                    Slug = link.Slug,
                    Summary = link.Summary,
                    Description = link.MetaDescription,
                    // Section links carry no hubType of their own, so fall back to the hub being rendered.
                    HubType = string.IsNullOrWhiteSpace(link.LandingPage?.Hub) ? hubType : link.LandingPage.Hub,
                    LandingPage = MapLandingPage(link.LandingPage),
                    CtaPanel = MapCtaPanel(link.CtaPanel)
                })
                .ToList();
        }

        private static List<HubStatistic> MapStatistics(List<ResponseHubStatistic> statistics)
        {
            if (statistics == null || !statistics.Any())
            {
                return new List<HubStatistic>();
            }

            return statistics
                .Where(statistic => statistic != null)
                .Select(statistic => new HubStatistic
                {
                    Text = statistic.Text,
                    HighlightValue = statistic.HighlightValue,
                    QuoteName = statistic.QuoteName,
                    QuoteRole = statistic.QuoteRole,
                    ReferenceText = statistic.ReferenceText
                })
                .Where(statistic => statistic.HasContent)
                .ToList();
        }

        private static CardLandingPage MapLandingPage(CardLandingPageResponse landingPage)
        {
            if (landingPage == null || string.IsNullOrWhiteSpace(landingPage.Slug))
            {
                return null;
            }

            return new CardLandingPage
            {
                Slug = landingPage.Slug,
                Title = landingPage.Title,
                Hub = landingPage.Hub,
                ParentSlug = landingPage.ParentSlug
            };
        }

        private static CtaPanel MapCtaPanel(ResponseCtaPanel ctaPanel)
        {
            if (ctaPanel == null)
            {
                return null;
            }

            return new CtaPanel
            {
                Heading = ctaPanel.Heading,
                Description = ctaPanel.Description,
                Icon = ctaPanel.Icon,
                ButtonText = ctaPanel.ButtonText,
                Url = ctaPanel.Url
            };
        }

        private static Image MapImage(Item image)
        {
            if (image?.EmbeddedResource == null)
            {
                return null;
            }

            return new Image
            {
                Description = image.EmbeddedResource.Description,
                Title = image.EmbeddedResource.Title,
                Url = image.EmbeddedResource.Url
            };
        }

        private static List<Card> MapCards(List<ResponseCard> cards)
        {
            if (cards == null || !cards.Any())
            {
                return new List<Card>();
            }

            return cards.Select(c => new Card
            {
                Title = c.Title,
                Slug = c.Slug,
                HubType = c.HubType,
                Summary = c.Summary,
                CardImage = new Image
                {
                    Description = c.CardImage?.EmbeddedResource?.Description,
                    Title = c.CardImage?.EmbeddedResource?.Title,
                    Url = c.CardImage?.EmbeddedResource?.Url
                }
            }).ToList();
        }
    }
}

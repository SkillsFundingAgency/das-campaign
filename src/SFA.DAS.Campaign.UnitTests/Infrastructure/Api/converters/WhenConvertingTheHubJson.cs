using System.IO;
using System.Linq;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingTheHubJson
    {
        private string jsonNoHeaderImage = "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"Find out how to become an apprentice, what apprenticeships are available and which employers offer them.\"},\"mainContent\":{\"headerImage\":null,\"cards\":[{\"pageType\":0,\"title\":\"Browse by interest\",\"metaDescription\":null,\"slug\":\"browse-by-interests\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what type of apprenticeships you can expect in your chosen interest.\"}]}, \"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";

        private const string json =
            "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"Find out how to become an apprentice, what apprenticeships are available and which employers offer them.\"},\"mainContent\":{\"headerImage\":{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"apprentice-sparks\",\"id\":\"7FMiFuKxmMQDmVxbhPQy4K\",\"fileName\":\"apprentice-sparks.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/7FMiFuKxmMQDmVxbhPQy4K/2d693fc0e6955f6a58bc12e663282a80/apprentice-sparks.jpg\",\"size\":57643,\"description\":null}},\"cardsTitle\":\"First group\",\"cards\":[{\"pageType\":0,\"title\":\"Browse by interest\",\"metaDescription\":null,\"slug\":\"browse-by-interests\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what type of apprenticeships you can expect in your chosen interest.\"}],\"cardsTitle2\":\"Second group\",\"cards2\":[{\"pageType\":0,\"title\":\"Browse by sector\",\"metaDescription\":null,\"slug\":\"browse-by-sector\",\"hubType\":\"Apprentices\",\"summary\":\"Find apprenticeships by sector.\"}],\"cardsTitle3\":\"Third group\",\"cards3\":[{\"pageType\":0,\"title\":\"Browse by location\",\"metaDescription\":null,\"slug\":\"browse-by-location\",\"hubType\":\"Apprentices\",\"summary\":\"Find apprenticeships by location.\"}]},\"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";


        // The same payload in the updated API shape, with mainContent.sections alongside the legacy cards.
        private static readonly string jsonWithSections = "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"Find out how to become an apprentice, what apprenticeships are available and which employers offer them.\"},\"mainContent\":{\"headerImage\":{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"apprentice-sparks\",\"id\":\"7FMiFuKxmMQDmVxbhPQy4K\",\"fileName\":\"apprentice-sparks.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/7FMiFuKxmMQDmVxbhPQy4K/2d693fc0e6955f6a58bc12e663282a80/apprentice-sparks.jpg\",\"size\":57643,\"description\":null}},\"cardsTitle\":\"First group\",\"cards\":[{\"pageType\":0,\"title\":\"Browse by interest\",\"metaDescription\":null,\"slug\":\"browse-by-interests\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what type of apprenticeships you can expect in your chosen interest.\"}],\"cardsTitle2\":\"Second group\",\"cards2\":[{\"pageType\":0,\"title\":\"Browse by sector\",\"metaDescription\":null,\"slug\":\"browse-by-sector\",\"hubType\":\"Apprentices\",\"summary\":\"Find apprenticeships by sector.\"}],\"cardsTitle3\":\"Third group\",\"cards3\":[{\"pageType\":0,\"title\":\"Browse by location\",\"metaDescription\":null,\"slug\":\"browse-by-location\",\"hubType\":\"Apprentices\",\"summary\":\"Find apprenticeships by location.\"}],\"sections\":[{\"sectionType\":\"Regular\",\"heading\":\"Is an apprenticeship right for you?\",\"introduction\":\"Check if an apprenticeship is right for you.\",\"image\":{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"How do they work\",\"id\":\"1OdrMNjy4KXbZUNxmAieO1\",\"fileName\":\"how-do-they-work.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/1OdrMNjy4KXbZUNxmAieO1/how-do-they-work.jpg\",\"size\":112182,\"description\":\"A section image\"},\"videoTranscripts\":null},\"stepperLinks\":[{\"id\":\"8cy2XVSZTWR7fMPCCE5uX\",\"pageType\":2,\"title\":\"Is an apprenticeship right for you?\",\"slug\":\"is-an-apprenticeship-right-for-you\",\"summary\":\"What to expect from an apprenticeship.\",\"metaDescription\":\"What to expect from an apprenticeship.\",\"landingPage\":{\"slug\":\"are-they-right-for-you\",\"title\":\"Are they right for you?\",\"hub\":\"Apprentices\",\"pageType\":null,\"parentSlug\":null},\"ctaPanel\":null},{\"id\":\"2ioxtbsIXtEtDNdPwfbS0y\",\"pageType\":1,\"title\":\"Browse by interests\",\"slug\":\"interests\",\"summary\":\"There are more than 700 different types of apprenticeship.\",\"metaDescription\":null,\"landingPage\":{\"slug\":null,\"title\":null,\"hub\":null,\"pageType\":null,\"parentSlug\":null},\"ctaPanel\":null}],\"standardLinks\":[{\"id\":\"3cnAtsdWapcHgI0LQ19a8l\",\"pageType\":2,\"title\":\"Get support if you have been in care\",\"slug\":\"support-care-experienced-apprentices\",\"summary\":\"Financial support towards your apprenticeship.\",\"metaDescription\":\"Financial support towards your apprenticeship.\",\"landingPage\":{\"slug\":\"are-they-right-for-you\",\"title\":\"Are they right for you?\",\"hub\":\"Apprentices\",\"pageType\":null,\"parentSlug\":null},\"ctaPanel\":null}],\"ctaPanel\":{\"heading\":\"Find an apprenticeship\",\"description\":\"Search and apply for apprenticeships.\",\"icon\":\"Search\",\"buttonText\":\"Find an apprenticeship\",\"url\":\"https://www.findapprenticeship.service.gov.uk/\"}}]},\"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";

        // A Stats section: entries with a highlight are statistics, entries without one are quotes.
        private static readonly string jsonWithStatsSection = "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"A summary.\"},\"mainContent\":{\"headerImage\":null,\"cards\":[],\"sections\":[{\"sectionType\":\"Stats\",\"heading\":\"Apprenticeships in numbers\",\"introduction\":\"What apprentices and employers say.\",\"image\":null,\"stepperLinks\":[],\"standardLinks\":[],\"statisticsSections\":[{\"text\":\"of employers said apprenticeships helped them develop relevant skills.\",\"highlightValue\":\"86%\",\"quoteName\":null,\"quoteRole\":null,\"referenceText\":\"Employer benefits survey\"},{\"text\":\"Taking on an apprentice was the best decision we made.\",\"highlightValue\":null,\"quoteName\":\"Jane\",\"quoteRole\":\"Industry Talent Specialist, Channel 4\",\"referenceText\":null},{\"text\":null,\"highlightValue\":\"   \",\"quoteName\":null,\"quoteRole\":null,\"referenceText\":null}],\"ctaPanel\":null}]},\"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";


        // The carousel the CMS now returns on mainContent: a plain array of image assets.
        private static readonly string jsonWithCarousel = "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"A summary.\"},\"mainContent\":{\"headerImage\":null,\"cards\":[],\"carousel\":[{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"Apprentice working in a lab\",\"id\":\"5KpQ2xJcYm1RtVbNwHZa3d\",\"fileName\":\"apprentice-lab.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/5KpQ2xJcYm1RtVbNwHZa3d/apprentice-lab.jpg\",\"size\":84213,\"description\":\"An apprentice working in a laboratory\"},\"videoTranscripts\":null},{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"Apprentice on a construction site\",\"id\":\"6LrS3yKdZn2SuWcOxIAb4e\",\"fileName\":\"apprentice-construction.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/6LrS3yKdZn2SuWcOxIAb4e/apprentice-construction.jpg\",\"size\":91544,\"description\":\"An apprentice on a construction site\"},\"videoTranscripts\":null},{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":null,\"videoTranscripts\":null}]},\"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";


        [Test, MoqAutoData]
        public void The_Page_Model_Is_Returned(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }
        
        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_Page_Information(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Slug.Should().NotBeNullOrWhiteSpace();
            actual.Title.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.MetaDescription.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.PageTitle.Should().NotBeNullOrWhiteSpace();
        }

        [Test, MoqAutoData]
        public void The_Header_Image_Is_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.HeaderImage.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Cards_Are_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Cards.Should().NotBeNullOrEmpty();
        }

        [Test, MoqAutoData]
        public void The_Card_Group_Titles_Are_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.CardsTitle.Should().Be("First group");
            actual.Content.CardsTitle2.Should().Be("Second group");
            actual.Content.CardsTitle3.Should().Be("Third group");
        }

        [Test, MoqAutoData]
        public void The_Second_And_Third_Card_Groups_Are_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Cards2.Should().NotBeNullOrEmpty();
            actual.Content.Cards3.Should().NotBeNullOrEmpty();
        }

        [Test, MoqAutoData]
        public void If_The_Header_Image_Is_Null_Then_Header_Image_Is_Not_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonNoHeaderImage);

            actual.Content.HeaderImage.Description.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Url.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Title.Should().BeNullOrWhiteSpace();
        }

        [Test, MoqAutoData]
        public void The_Sections_Are_Set_From_The_Updated_Api_Model(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            actual.Content.Sections.Should().HaveCount(1);

            var section = actual.Content.Sections.Single();
            section.SectionType.Should().Be("Regular");
            section.Heading.Should().Be("Is an apprenticeship right for you?");
            section.Introduction.Should().Be("Check if an apprenticeship is right for you.");
            section.Image.Url.Should().Be("https://images.ctfassets.net/8kbr1n52z8s2/1OdrMNjy4KXbZUNxmAieO1/how-do-they-work.jpg");
            section.Image.Description.Should().Be("A section image");
        }

        [Test, MoqAutoData]
        public void The_Section_Links_Are_Split_Into_Stepper_And_Standard_Links(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            var section = actual.Content.Sections.Single();

            section.StepperLinks.Should().HaveCount(2);
            section.StandardLinks.Should().HaveCount(1);
            section.StandardLinks.Single().Title.Should().Be("Get support if you have been in care");
        }

        [Test, MoqAutoData]
        public void The_Section_Link_Url_Uses_The_Landing_Page_Hub(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            var link = actual.Content.Sections.Single().StepperLinks.First();

            link.HubType.Should().Be("Apprentices");
            link.Url.Should().Be("/Apprentices/is-an-apprenticeship-right-for-you");
            link.LandingPage.Slug.Should().Be("are-they-right-for-you");
        }

        [Test, MoqAutoData]
        public void The_Section_Link_Url_Falls_Back_To_The_Page_Hub_When_There_Is_No_Landing_Page(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            var link = actual.Content.Sections.Single().StepperLinks.Last();

            link.HubType.Should().Be("Apprentices");
            link.Url.Should().Be("/Apprentices/interests");
            link.LandingPage.Should().BeNull();
        }

        [Test, MoqAutoData]
        public void The_Section_Cta_Panel_Is_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            var ctaPanel = actual.Content.Sections.Single().CtaPanel;

            ctaPanel.Should().NotBeNull();
            ctaPanel.Heading.Should().Be("Find an apprenticeship");
            ctaPanel.Icon.Should().Be("Search");
            ctaPanel.ButtonText.Should().Be("Find an apprenticeship");
            ctaPanel.Url.Should().Be("https://www.findapprenticeship.service.gov.uk/");
        }

        [Test, MoqAutoData]
        public void The_Legacy_Cards_Are_Still_Mapped_When_Sections_Are_Present(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            actual.Content.Cards.Should().NotBeNullOrEmpty();
            actual.Content.CardsTitle.Should().Be("First group");
        }

        [Test, MoqAutoData]
        public void Sections_Are_Empty_When_The_Api_Does_Not_Return_Them(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Sections.Should().NotBeNull();
            actual.Content.Sections.Should().BeEmpty();
        }

        [Test, MoqAutoData]
        public void A_Stats_Section_Is_Identified_By_Its_Section_Type(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithStatsSection);

            var section = actual.Content.Sections.Single();

            section.SectionType.Should().Be("Stats");
            section.IsStatsSection.Should().BeTrue();
            section.Heading.Should().Be("Apprenticeships in numbers");
        }

        [Test, MoqAutoData]
        public void A_Statistic_With_A_Highlight_Is_A_Stat(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithStatsSection);

            var statistic = actual.Content.Sections.Single().Statistics.First();

            statistic.IsStat.Should().BeTrue();
            statistic.HighlightValue.Should().Be("86%");
            statistic.Text.Should().Be("of employers said apprenticeships helped them develop relevant skills.");
            statistic.ReferenceText.Should().Be("Employer benefits survey");
        }

        [Test, MoqAutoData]
        public void A_Statistic_Without_A_Highlight_Is_A_Quote(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithStatsSection);

            var statistic = actual.Content.Sections.Single().Statistics.Last();

            statistic.IsStat.Should().BeFalse();
            statistic.HighlightValue.Should().BeNull();
            statistic.Text.Should().Be("Taking on an apprentice was the best decision we made.");
            statistic.QuoteName.Should().Be("Jane");
            statistic.QuoteRole.Should().Be("Industry Talent Specialist, Channel 4");
        }

        [Test, MoqAutoData]
        public void Empty_Statistics_Are_Not_Returned(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithStatsSection);

            actual.Content.Sections.Single().Statistics.Should().HaveCount(2);
        }

        [Test, MoqAutoData]
        public void Statistics_Are_Empty_When_A_Section_Does_Not_Return_Them(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithSections);

            var section = actual.Content.Sections.Single();

            section.IsStatsSection.Should().BeFalse();
            section.Statistics.Should().NotBeNull();
            section.Statistics.Should().BeEmpty();
        }

        [Test, MoqAutoData]
        public void The_Carousel_Images_Are_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithCarousel);

            actual.Content.Carousel.Should().HaveCount(2);

            var image = actual.Content.Carousel.First();
            image.Title.Should().Be("Apprentice working in a lab");
            image.Url.Should().Be("https://images.ctfassets.net/8kbr1n52z8s2/5KpQ2xJcYm1RtVbNwHZa3d/apprentice-lab.jpg");
            image.Description.Should().Be("An apprentice working in a laboratory");
        }

        [Test, MoqAutoData]
        public void A_Carousel_Entry_Without_An_Asset_Is_Not_Returned(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonWithCarousel);

            actual.Content.Carousel.Should().OnlyContain(image => image.Url != null);
        }

        [Test, MoqAutoData]
        public void The_Carousel_Is_Empty_When_The_Api_Does_Not_Return_It(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Carousel.Should().NotBeNull();
            actual.Content.Carousel.Should().BeEmpty();
        }

        [TestCase("Apprentices", HubType.Apprentices)]
        [TestCase("Employers", HubType.Employers)]
        [TestCase("apprentices", HubType.Apprentices)]
        [TestCase("EMPLOYERS", HubType.Employers)]
        public void The_Hub_Type_Is_Parsed_Regardless_Of_Casing(string hubType, HubType expected)
        {
            var converter = new HubJsonConverter(Mock.Of<IHtmlControlAbstractFactory>());

            var actual = InvokeReadJsonMethodOnConverter(converter, JsonWithHubType(hubType));

            actual.HubType.Should().Be(expected);
        }

        [TestCase("Influencers", Description = "Retired hub that the CMS still serves")]
        [TestCase("Parents")]
        [TestCase("something-the-cms-invented")]
        [TestCase("7")]
        [TestCase("")]
        public void An_Unrecognised_Hub_Type_Returns_No_Page_So_The_Controller_Renders_Not_Found(string hubType)
        {
            var converter = new HubJsonConverter(Mock.Of<IHtmlControlAbstractFactory>());

            var actual = InvokeReadJsonMethodOnConverter(converter, JsonWithHubType(hubType));

            actual.Should().BeNull();
        }

        [Test, MoqAutoData]
        public void A_Missing_Hub_Type_Returns_No_Page_Rather_Than_Throwing(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, JsonWithHubType(null));

            actual.Should().BeNull();
        }

        private static string JsonWithHubType(string hubType)
        {
            var value = hubType == null ? "null" : $"\"{hubType}\"";

            return json.Replace("\"hubType\":\"Apprentices\"", $"\"hubType\":{value}");
        }

        private static Page<Hub> InvokeReadJsonMethodOnConverter(HubJsonConverter converter, string jsonToUse = json)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(jsonToUse)), typeof(Page<Hub>), "",
                new Mock<JsonSerializer>().Object) as Page<Hub>;
            return actual;
        }
    }
}

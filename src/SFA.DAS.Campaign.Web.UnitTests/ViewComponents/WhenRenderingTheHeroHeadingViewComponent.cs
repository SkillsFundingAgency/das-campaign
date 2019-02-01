using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.ViewComponents.Button;
using SFA.DAS.Campaign.Web.ViewComponents.Form;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using SFA.DAS.Campaign.Web.Models.Components.Form;
using SFA.DAS.Campaign.Web.ViewComponents;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class WhenRenderingTheHeroHeadingViewComponent
    {
        private HeroHeadingViewComponent _sut;

        [SetUp]
        public void Arrange()
        {
            _sut = new HeroHeadingViewComponent();
        }

        [Test]
        public async Task When_Default_Then_The_Type_Is_None_And_No_Caption()
        {
            var headingType = HeroHeadingType.None;
            //Act
            var actual = await _sut.InvokeAsync(headingType, null,null,null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default",result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel) result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.IsNull(headingViewModel.Caption);

        }

        [Test]
        public async Task When_Apprentice_Then_The_Type_Is_Apprentice_And_Caption_Is_Apprentice()
        {
            var headingType = HeroHeadingType.Apprentice;
            var caption = "Apprentice";
            //Act
            var actual = await _sut.InvokeAsync(headingType, null, null, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(caption,headingViewModel.Caption);
        }

        [Test]
        public async Task When_Employer_Then_The_Type_Is_Employer_And_Caption_Is_Employer()
        {
            var headingType = HeroHeadingType.Employer;
            var caption = "Employer";
            //Act
            var actual = await _sut.InvokeAsync(headingType, null, null, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(caption, headingViewModel.Caption);
        }

        [Test]
        public async Task When_Employer_And_Custom_Caption_Then_The_Type_Is_Employer_And_Caption_Custom()
        {
            var headingType = HeroHeadingType.Employer;
            var caption = "Custom";
            //Act
            var actual = await _sut.InvokeAsync(headingType, caption, null, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(caption, headingViewModel.Caption);
        }

        [Test]
        public async Task When_Apprentice_And_Custom_Caption_Then_The_Type_Is_Apprentice_And_Caption_Custom()
        {
            var headingType = HeroHeadingType.Apprentice;
            var caption = "Custom";
            //Act
            var actual = await _sut.InvokeAsync(headingType, caption, null, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(caption, headingViewModel.Caption);
        }

        [Test]
        public async Task When_Employer_And_Custom_Class_Then_The_Type_Is_Employer_And_Class_Custom()
        {
            var headingType = HeroHeadingType.Employer;
            var customClasses = "custom-employer-class";

            var actualClass = "hero-heading__caption--employer " + customClasses;
            //Act
            var actual = await _sut.InvokeAsync(headingType, null, customClasses, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(actualClass, headingViewModel.Class);
        }

        [Test]
        public async Task When_Apprentice_And_Custom_Class_Then_The_Type_Is_Apprentice_And_Class_Custom()
        {
            var headingType = HeroHeadingType.Apprentice;
            var customClasses = "custom-apprentice-class";
            var actualClass = "hero-heading__caption--apprentice " + customClasses;
            //Act
            var actual = await _sut.InvokeAsync(headingType, null, customClasses, null,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(actualClass, headingViewModel.Class);
        }

        [Test]
        public async Task When_Content_Then_Content_Is_Populated()
        {
            var headingType = HeroHeadingType.Employer;
            var content = customContent();
            //Act
            var actual = await _sut.InvokeAsync(headingType, null, null, content,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(headingType, headingViewModel.Type);
            Assert.AreEqual(content.ToString(), headingViewModel.Content.ToString());
        }


        private IHtmlContent customContent()
        {

            return new HtmlString("<p>Some custom conent</p>");
        }
    }
}

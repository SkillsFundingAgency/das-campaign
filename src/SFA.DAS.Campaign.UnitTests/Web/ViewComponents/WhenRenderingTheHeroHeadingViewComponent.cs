using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
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
        public void When_Default_Then_The_Type_Is_None_And_No_Caption()
        {
            var headingType = HeroHeadingType.None;
            //Act
            var actual = _sut.Invoke(headingType, null,null,null,null, null, null);

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
        public void When_Apprentice_Then_The_Type_Is_Apprentice_And_Caption_Is_Apprentice()
        {
            var headingType = HeroHeadingType.Apprentice;
            var caption = "Apprentice";
            //Act
            var actual = _sut.Invoke(headingType, null, null, null,null, null, null);

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
        public void When_Employer_Then_The_Type_Is_Employer_And_Caption_Is_Employer()
        {
            var headingType = HeroHeadingType.Employer;
            var caption = "Employer";
            //Act
            var actual = _sut.Invoke(headingType, null, null, null,null, null, null);

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
        public void When_Employer_And_Custom_Caption_Then_The_Type_Is_Employer_And_Caption_Custom()
        {
            var headingType = HeroHeadingType.Employer;
            var caption = "Custom";
            //Act
            var actual = _sut.Invoke(headingType, caption, null, null, null, null, null);

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
        public void When_Apprentice_And_Custom_Caption_Then_The_Type_Is_Apprentice_And_Caption_Custom()
        {
            var headingType = HeroHeadingType.Apprentice;
            var caption = "Custom";

            //Act
            var actual = _sut.Invoke(headingType, caption, null, null, null, null, null);

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
        public void When_Employer_And_Custom_Class_Then_The_Type_Is_Employer_And_Class_Custom()
        {
            var headingType = HeroHeadingType.Employer;
            var customClasses = "custom-employer-class";

            var actualClass = "hero-heading__caption--employer " + customClasses;
            //Act
            var actual = _sut.Invoke(headingType, null, customClasses, null, null, null, null);

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
        public void When_Apprentice_And_Custom_Class_Then_The_Type_Is_Apprentice_And_Class_Custom()
        {
            var headingType = HeroHeadingType.Apprentice;
            var customClasses = "custom-apprentice-class";
            var actualClass = "hero-heading__caption--apprentice " + customClasses;
            //Act
            var actual = _sut.Invoke(headingType, null, customClasses, null, null, null, null);

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
        public void When_Content_Then_Content_Is_Populated()
        {
            var headingType = HeroHeadingType.Employer;
            var content = customContent();
            //Act
            var actual = _sut.Invoke(headingType, null, null, content, null, null, null);

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

        [Test]
        public void When_Employer_With_Image_Then_Image_Is_Added_To_Header()
        {
            var headingType = HeroHeadingType.EmployerWithImage;
            var content = customContent();
            //Act
            var actual = _sut.Invoke(headingType, null, null, content, null, "testimage.jpg", "Header image description");

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(HeroHeadingType.Employer, headingViewModel.Type);
            Assert.AreEqual(content.ToString(), headingViewModel.Content.ToString());
            Assert.AreEqual("testimage.jpg", headingViewModel.ImageUrl);
            Assert.AreEqual("Header image description", headingViewModel.ImageAltText);
        }

        private IHtmlContent customContent()
        {

            return new HtmlString("<p>Some custom conent</p>");
        }
    }
}

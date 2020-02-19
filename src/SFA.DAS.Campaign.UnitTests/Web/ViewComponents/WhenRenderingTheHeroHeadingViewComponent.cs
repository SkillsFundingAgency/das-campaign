using System;
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
        private HeroHeadingViewModel _sutModel;

        [SetUp]
        public void Arrange()
        {
            _sut = new HeroHeadingViewComponent();
            _sutModel = new HeroHeadingViewModel();
            

        }

        [Test]
        public void When_Default_Then_The_Type_Is_None_And_No_Caption()
        {
            _sutModel.Type = HeroHeadingType.None;
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default",result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel) result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(String.Empty,headingViewModel.Caption);

        }

        [Test]
        public void When_Apprentice_Then_The_Type_Is_Apprentice_And_Caption_Is_Apprentice()
        {
            _sutModel.Type = HeroHeadingType.Apprentice;
            _sutModel.Caption = "Apprentice";
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Caption, headingViewModel.Caption);
        }

        [Test]
        public void When_Employer_Then_The_Type_Is_Employer_And_Caption_Is_Employer()
        {
            _sutModel.Type = HeroHeadingType.Apprentice;
            _sutModel.Caption = "Employer";
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Caption, headingViewModel.Caption);
        }

        [Test]
        public void When_Employer_And_Custom_Caption_Then_The_Type_Is_Employer_And_Caption_Custom()
        {
            _sutModel.Type = HeroHeadingType.Employer;
            _sutModel.Caption = "Custom";
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Caption, headingViewModel.Caption);
        }

        [Test]
        public void When_Apprentice_And_Custom_Caption_Then_The_Type_Is_Apprentice_And_Caption_Custom()
        {
            _sutModel.Type = HeroHeadingType.Apprentice;
            _sutModel.Caption = "Custom";

            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Caption, headingViewModel.Caption);
        }

        [Test]
        public void When_Employer_And_Custom_Class_Then_The_Type_Is_Employer_And_Class_Custom()
        {
            _sutModel.Type = HeroHeadingType.Employer;
            var customClass = "custom-employer-class";
            _sutModel.Class = customClass;
            var actualClass = "hero-heading__caption--employer " + customClass;

            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(actualClass, headingViewModel.Class);
        }

        [Test]
        public void When_Apprentice_And_Custom_Class_Then_The_Type_Is_Apprentice_And_Class_Custom()
        {
            _sutModel.Type = HeroHeadingType.Apprentice;
            var customClass = "custom-apprentice-class";
            _sutModel.Class = customClass;
            var actualClass = "hero-heading__caption--apprentice " + customClass;
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(actualClass, headingViewModel.Class);
        }

        [Test]
        public void When_Content_Then_Content_Is_Populated()
        {
            _sutModel.Type = HeroHeadingType.Apprentice;
            _sutModel.Content = customContent();
            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.ViewName);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(_sutModel.Type, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Content.ToString(), headingViewModel.Content.ToString());
        }

        [Test]
        public void When_Employer_With_Image_Then_Image_Is_Added_To_Header()
        {
            _sutModel.Type = HeroHeadingType.Employer;
            _sutModel.Content = customContent();
            _sutModel.ImageUrl = "testimage.jpg";
            _sutModel.ImageAltText = "Header image description";



            //Act
            var actual = _sut.Invoke(_sutModel);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<HeroHeadingViewModel>(result.ViewData.Model);
            var headingViewModel = (HeroHeadingViewModel)result.ViewData.Model;

            Assert.AreEqual(HeroHeadingType.Employer, headingViewModel.Type);
            Assert.AreEqual(_sutModel.Content.ToString(), headingViewModel.Content.ToString());
            Assert.AreEqual(_sutModel.ImageUrl, headingViewModel.ImageUrl);
            Assert.AreEqual(_sutModel.ImageAltText, headingViewModel.ImageAltText);
        }

        private IHtmlContent customContent()
        {

            return new HtmlString("<p>Some custom conent</p>");
        }
    }
}

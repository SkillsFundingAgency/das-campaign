using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.ViewComponents.Button;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class WhenRenderingTheButtonViewComponent
    { 
        private ButtonViewComponent _sut;
        private string _buttonName = "button";

        [SetUp]
        public void Arrange()
        {
          _sut = new ButtonViewComponent();
        }

        [Test]
        public void Then_The_Correct_Button_Text_Is_Displayed()
        {
            var buttonText = "Button";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Apprentice, _buttonName, buttonText);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Anchor", result.ViewName);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel) result.ViewData.Model;

            Assert.AreEqual(buttonText, buttonViewModel.Text);
        }

        [Test]
        public void When_Apprentice_And_Shadow_And_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Apprentice";
            var classes = "button button-apprentice button--sparks button--shadow ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Apprentice, _buttonName, buttonText,shadow: true, sparks:true);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_Apprentice_And_Shadow_And_No_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Apprentice";
            var classes = "button button-apprentice button--shadow ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: true, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_Apprentice_And_No_Shadow_And_No_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Apprentice";
            var classes = "button button-apprentice ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_Apprentice_And_No_Shadow_And_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Apprentice";
            var classes = "button button-apprentice button--sparks ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: false, sparks: true);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_Employer_And_No_Shadow_And_No_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Apprentice";
            var classes = "button button-employer ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Employer, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_Primary_And_No_Shadow_And_No_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Primary";
            var classes = "button ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.Primary, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }
        [Test]
        public void When_PrimaryInverted_And_No_Shadow_And_No_Sparks_Then_The_Correct_Classes_Applied()
        {
            var buttonText = "Primary";
            var classes = "button button-inverted ";
            //Act
            var actual = _sut.Invoke(ButtonType.Anchor, ButtonStyle.PrimaryInverted, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;

            Assert.AreEqual(classes, buttonViewModel.GetClasses());
        }

        [Test]
        public void When_Button_Type_Is_Anchor_Then_The_Correct_View()
        {
            var buttonText = "Primary";
            var buttonType = ButtonType.Anchor;
            //Act
            var actual = _sut.Invoke(buttonType, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("Anchor", result.ViewName);


            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;
            Assert.AreEqual(buttonType, buttonViewModel.Type);
        }

        [Test]
        public void When_Button_Type_Is_Input_Then_The_Correct_View()
        {
            var buttonText = "Primary";
            var buttonType = ButtonType.Input;
            //Act
            var actual = _sut.Invoke(buttonType, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("Input", result.ViewName);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;
            Assert.AreEqual(buttonType, buttonViewModel.Type);
        }

        [Test]
        public void When_Button_Type_Is_Button_Then_The_Correct_View()
        {
            var buttonText = "Primary";
            var buttonType = ButtonType.Button;
            //Act
            var actual = _sut.Invoke(buttonType, ButtonStyle.Apprentice, _buttonName, buttonText, shadow: false, sparks: false);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("Button", result.ViewName);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;
            Assert.AreEqual(buttonType, buttonViewModel.Type);
        }

        [Test]
        public void When_Href_Supplied_Then_Model_Href_Populated()
        {
            var buttonText = "Primary";
            var buttonType = ButtonType.Button;
            string href = "/index.html";
            //Act
            var actual = _sut.Invoke(buttonType, ButtonStyle.Apprentice, _buttonName, buttonText, href: href );

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;
            Assert.AreEqual(href, buttonViewModel.Href);
        }

        [Test]
        public void When_No_Href_Supplied_Then_Model_Href_Default()
        {
            var buttonText = "Primary";
            var buttonType = ButtonType.Button;
            string href = "#";
            //Act
            var actual = _sut.Invoke(buttonType, ButtonStyle.Apprentice, _buttonName, buttonText);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ButtonViewModel>(result.ViewData.Model);
            var buttonViewModel = (ButtonViewModel)result.ViewData.Model;
            Assert.AreEqual(href, buttonViewModel.Href);
        }
    }
}

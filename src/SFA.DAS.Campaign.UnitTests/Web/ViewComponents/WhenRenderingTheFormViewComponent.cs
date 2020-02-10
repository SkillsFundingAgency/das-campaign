using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.ViewComponents.Form;
using SFA.DAS.Campaign.Web.Models.Components.Form;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class WhenRenderingTheFormViewComponent
    {
        private FormViewComponent _sut;
   
        [SetUp]
        public void Arrange()
        {
            _sut = new FormViewComponent();
        }

        [Test]
        public void When_RegisterInterest_Then_The_Correct_View_Returned()
        {

            //Act
            var actual = _sut.Invoke(FormType.RegisterInterest, null, null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("RegisterInterest", result.ViewName);
        }
        [Test]
        public void When_RegisterInterest_Then_The_Correct_Model_Returned()
        {

            //Act
            var actual = _sut.Invoke(FormType.RegisterInterest, null, null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<RegisterInterestModel>(result.ViewData.Model);
            var formViewModel = (RegisterInterestModel)result.ViewData.Model;

            Assert.IsNotNull(formViewModel);
        }

        [TestCase("/", "Home", "Index", "0")]
        [TestCase("/parents/test", "parents", "test", "1")]
        [TestCase("/apprentice/test", "apprentice", "test", "1")]
        [TestCase("/test/apprentice", "test", "apprentice", "1")]
        [TestCase("/employer/test", "employer", "test", "2")]
        [TestCase("/test/employer", "test", "employer", "2")]
        public void When_RegisterInterest_The_Apprentice_Or_Employer_Question_Is_Preselected_And_Hidden_If_Directed_From_A_Known_Page
            (string returnUrl, string controllerName, string actionName, string expectedRoute)
        {
            //Act
            var actual = _sut.Invoke(FormType.RegisterInterest, new RegisterInterestModel(returnUrl, 1, controllerName, actionName), null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<RegisterInterestModel>(result.ViewData.Model);

            var formViewModel = (RegisterInterestModel)result.ViewData.Model;
            Assert.AreEqual(expectedRoute, formViewModel.Route);
            Assert.AreEqual(returnUrl, formViewModel.ReturnUrl);
        }
    }
}

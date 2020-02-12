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

        [TestCase("/", null)]
        [TestCase("/parents/test", "1")]
        [TestCase("/apprentice/test", "1")]
        [TestCase("/test/apprentice", "1")]
        [TestCase("/employer/test", "2")]
        [TestCase("/test/employer", "2")]
        public void When_RegisterInterest_The_Apprentice_Or_Employer_Question_Is_Preselected_And_Hidden_If_Directed_From_A_Known_Page
            (string returnUrl, string route)
        {
            //Act
            var actual = _sut.Invoke(FormType.RegisterInterest, new RegisterInterestModel(returnUrl, 1, route), null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<RegisterInterestModel>(result.ViewData.Model);

            var formViewModel = (RegisterInterestModel)result.ViewData.Model;
            Assert.AreEqual(returnUrl, formViewModel.ReturnUrl);
            Assert.AreEqual(route, formViewModel.Route);
        }
    }
}

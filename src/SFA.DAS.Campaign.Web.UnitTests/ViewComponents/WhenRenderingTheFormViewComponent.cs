using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.ViewComponents.Button;
using SFA.DAS.Campaign.Web.ViewComponents.Form;
using System.Threading.Tasks;
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
        public async Task When_RegisterInterest_Then_The_Correct_View_Returned()
        {

            //Act
            var actual = await _sut.InvokeAsync(FormType.RegisterInterest,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("RegisterInterest",result.ViewName);

        }
        [Test]
        public async Task When_RegisterInterest_Then_The_Correct_Model_Returned()
        {

            //Act
            var actual = await _sut.InvokeAsync(FormType.RegisterInterest,null);

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<RegisterInterestModel>(result.ViewData.Model);
            var formViewModel = (RegisterInterestModel)result.ViewData.Model;

            Assert.IsNotNull(formViewModel);

        }

    }
}

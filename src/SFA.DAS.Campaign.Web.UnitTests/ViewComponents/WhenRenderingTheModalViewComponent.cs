using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.Home
{
    [TestFixture]
    public class WhenRenderingTheModalViewComponent
    {
        private ModalViewComponent _sut;
        private string _id = "modal";
        private string _componentName = "testComponent";
        private string _partialViewName = "testPartialView";

        [SetUp]
        public void Arrange()
        {
            _sut = new ModalViewComponent();
        }

        [Test]
        public async Task When_Component_Then_The_ModalType_Is_Component()
        {

            //Act
            var actual = await _sut.InvokeAsync(_id,ModalType.Component,_componentName,new {});

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Modal", result.ViewName);

            Assert.IsInstanceOf<ModalViewModel>(result.ViewData.Model);
            var modalViewModel = (ModalViewModel)result.ViewData.Model;

            Assert.AreEqual(ModalType.Component,modalViewModel.Type);
            Assert.AreEqual(_componentName,modalViewModel.Name);
            Assert.AreEqual(_id,modalViewModel.Id);

        }
        [Test]
        public async Task When_Component_Then_The_ModalType_Is_PartialView()
        {

            //Act
            var actual = await _sut.InvokeAsync(_id, ModalType.PartialView, _partialViewName, new { });

            //Assert
            Assert.IsNotNull(actual);
            var result = actual as ViewViewComponentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Modal", result.ViewName);

            Assert.IsInstanceOf<ModalViewModel>(result.ViewData.Model);
            var modalViewModel = (ModalViewModel)result.ViewData.Model;

            Assert.AreEqual(ModalType.PartialView, modalViewModel.Type);
            Assert.AreEqual(_partialViewName, modalViewModel.Name);
            Assert.AreEqual(_id, modalViewModel.Id);
        }

    }
}

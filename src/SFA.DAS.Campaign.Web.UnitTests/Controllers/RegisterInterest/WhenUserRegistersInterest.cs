using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.DataCollection;
using SFA.DAS.Campaign.Web.Controllers;
using SFA.DAS.Campaign.Web.Models;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace SFA.DAS.Campaign.Web.UnitTests.Controllers.RegisterInterest
{
    public class WhenUserRegistersInterest
    {
        private Mock<IUserDataCollection> _userDataCollection;
        private RegisterInterestController _controller;
        private Mock<HttpContext> _httpContext;
        private RegisterInterestModel _registerInterestModel;

        private const string ExpectedCookieId = "123FDSF.123";
        private const string ExpectedReferrerUrl = "http://test/cpg/test";
        private const string ExpectedDefaultUrl = "http://test/cpg/test";

        [SetUp]
        public void Arrange()
        {
            _registerInterestModel = new RegisterInterestModel
            {
                Email = "a@a.com",
                FirstName = "Test",
                Route = "1",
                LastName = "test",
                AcceptTandCs = true,
            };
            _userDataCollection = new Mock<IUserDataCollection>();

            var cookies = new RequestCookieCollection(new Dictionary<string, string>{{ "_ga", ExpectedCookieId } } );
            var headers = new HeaderDictionary(new Dictionary<string, StringValues>{{ "Referer", ExpectedReferrerUrl } } );
            _httpContext = new Mock<HttpContext>();
            _httpContext.Setup(x => x.Request.Cookies).Returns(cookies);
            _httpContext.Setup(x => x.Request.Headers).Returns(headers);
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {HttpContext = _httpContext.Object,
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ControllerName = "register-interest"
                }}
            };
        }

        [Test]
        public void Then_When_Viewing_The_Form_The_Referring_Url_Is_Taken()
        {
            //Act
            var actual = _controller.Index();

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(ExpectedReferrerUrl, model.ReturnUrl);
        }

        [Test]
        public void Then_If_The_Referrer_Is_Itself_Then_It_Is_Redirected_To_The_Homepage()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                    { "Referer", "https://test/Register-interest" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                    HttpContext = _httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = "register-interest"
                    }}
            };

            //Act
            var actual = _controller.Index();

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(ExpectedDefaultUrl, model.ReturnUrl);
        }

        [Test]
        public void Then_When_Viewing_The_RegisterInterest_Form_If_There_Is_No_Referrer_They_Are_Redirected_To_The_HomePage()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers).Returns(new HeaderDictionary(new Dictionary<string, StringValues>()));
            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = { HttpContext = _httpContext.Object }
            };

            //Act
            var actual = _controller.Index();

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(ExpectedDefaultUrl, model.ReturnUrl);
            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("Index") && c.Controller.Equals("Home"))));
        }

        [Test]
        public void Then_When_Viewing_The_RegisterInterest_Form_Only_The_Controller_And_Action_Segments_Are_Used()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                    { "Referer", "https://test2/some-controller/some" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                    HttpContext = _httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = "register-interest"
                    }}
            };

            //Act
            var actual = _controller.Index();

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.IsNotNull(model);

            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("some") && c.Controller.Equals("some-controller"))));
        }

        [Test]
        public void Then_When_Viewing_The_RegisterInterest_Form_Then_If_There_Are_No_Controller_Action_Segments_Default_Values_AreUsed()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                    { "Referer", "https://test2/" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                    HttpContext = _httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = "register-interest"
                    }}
            };

            //Act
            var actual = _controller.Index();

            //Assert
            Assert.IsNotNull(actual);
            var viewResult = actual as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.IsNotNull(model);
            
            mockUrlHelper.Verify(x=>x.Action(It.Is<UrlActionContext>(c=>c.Action.Equals("Index") && c.Controller.Equals("Home"))));
        }

        [Test]
        public async Task Then_StoreData_Is_Called_On_The_UserDataCollection_Service()
        {
            //Act
            await _controller.Index(_registerInterestModel);

            //Assert
            _userDataCollection.Verify(x=>x.StoreUserData(It.Is<UserData>(
                c=>c.Email.Equals(_registerInterestModel.Email) &&
                   c.FirstName.Equals(_registerInterestModel.FirstName) &&
                   c.LastName.Equals(_registerInterestModel.LastName) &&
                   c.Consent.Equals(_registerInterestModel.AcceptTandCs) &&
                   c.RouteId.Equals(_registerInterestModel.Route) &&
                   c.CookieId.Equals(ExpectedCookieId)
                )), Times.Once);
        }

        [Test]
        public async Task Then_If_The_Ga_Cookie_Is_Not_Available_A_Default_Value_Is_Used()
        {
            //Arrange
            _httpContext = new Mock<HttpContext>();
            _httpContext.Setup(x => x.Request.Cookies)
                .Returns(new RequestCookieCollection(new Dictionary<string, string> {  }));

            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                ControllerContext = { HttpContext = _httpContext.Object }
            };
            
            //Act
            await _controller.Index(_registerInterestModel);

            //Assert
            _userDataCollection.Verify(x => x.StoreUserData(It.Is<UserData>(
                c => c.CookieId.Equals("not-available")
            )), Times.Once);
        }

        [Test]
        public async Task Then_If_The_Model_Is_Not_Valid_Then_The_User_Data_Collection_Service_Is_Not_Called()
        {
            //Arrange
            _controller.ModelState.AddModelError("FirstName","First name");
            
            //Act
            await _controller.Index(_registerInterestModel);

            //Assert
            _userDataCollection.Verify(x => x.StoreUserData(It.IsAny<UserData>()), Times.Never);
        }

        [Test]
        public async Task Then_If_The_UserDataCollection_Fails_Validation_The_Errors_Are_Returned_To_The_View()
        {
            //Arrange
            _registerInterestModel.Email = "a@a";
            _userDataCollection
                .Setup(x => x.StoreUserData(It.IsAny<UserData>()))
                .ThrowsAsync(new ValidationException(new ValidationResult("Failed", new List<string>{"Email|The Email field is not valid." }),null,null));

            //Act
            var actual = await _controller.Index(_registerInterestModel);

            //Assert
            Assert.IsNotNull(actual);
            var actualViewResult = actual as ViewResult;
            Assert.IsNotNull(actualViewResult);
            Assert.IsFalse(actualViewResult.ViewData.ModelState.IsValid);
            Assert.IsTrue(actualViewResult.ViewData.ModelState.ContainsKey("Email"));
        }
    }
}

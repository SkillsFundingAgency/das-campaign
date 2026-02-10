using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Web.Controllers;
using SFA.DAS.Campaign.Web.Models;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace SFA.DAS.Campaign.UnitTests.Web.Controllers.RegisterInterest
{
    public class RegisterInterestController_Tests
    {
       
        private Mock<IUserDataCollection> _userDataCollection;
        private RegisterInterestController _controller;
        private Mock<HttpContext> _httpContext;
        private RegisterInterestModel _registerInterestModel;
        private Mock<IMediator> _mediator;

        private const string ExpectedCookieId = "123FDSF.123";
        private const string ExpectedReferrerUrl = "http://test/cpg/test";
        private const string ExpectedDefaultUrl = "http://test/cpg/test";
        private const string ExpectedVacancySearchReferrerURl = "http://test/cpg/SearchResults/standard/postcode/distance";
        
        [SetUp]
        public void Arrange()
        {
            _registerInterestModel = new RegisterInterestModel
            {
                Email = "a@a.com",
                FirstName = "Test",
                Route = RouteType.Apprentice,
                LastName = "test",
                SizeOfYourCompany="10",
                Industry = "test",
                Location = "test",
                ShowRouteQuestion = false
            };

            _mediator = new Mock<IMediator>();
            _mediator.SetupMockMediator();

            _userDataCollection = new Mock<IUserDataCollection>();
            
            var headers = new HeaderDictionary(new Dictionary<string, StringValues>{{ "Referer", ExpectedReferrerUrl } } );
            _httpContext = new Mock<HttpContext>();
            _httpContext.Setup(x => x.Request.Cookies["_ga"]).Returns(ExpectedCookieId);
            _httpContext.Setup(x => x.Request.Headers).Returns(headers);
            _httpContext.Setup(x => x.Request.Path).Returns("/");

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(ExpectedDefaultUrl).Verifiable();
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                    HttpContext = _httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = "register-interest"
                    },
                    RouteData = new RouteData()
                }
            };

        }

        [Test]
        public async Task Then_The_Index_View_Is_Returned()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Then_The_Model_Is_Populated()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.Menu, Is.Not.EqualTo(null));
            Assert.That(model.BannerModels, Is.Not.EqualTo(null));
        }

        [Test]
        public async Task Then_The_Version_Is_Populated()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.Version, Is.EqualTo(1));
        }

        [Test]
        public async Task Then_The_Route_Is_Populated()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.Route, Is.EqualTo(RouteType.Apprentice));
        }

        [Test]
        public async Task Then_The_ShowRouteQuestion_Is_False()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);
    
            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.ShowRouteQuestion, Is.EqualTo(false));
        }

        [Test]
        public async Task Then_The_ShowRouteQuestion_Is_False_If_Route_Is_Not_Known()
        {
            //Act
            var actual = await _controller.Index(RouteType.None);
            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.ShowRouteQuestion, Is.EqualTo(false));
        }

        [Test]
        public async Task Then_The_ReturnUrl_Is_Populated()
        {
            //Act
            var actual = await _controller.Index(RouteType.Apprentice);
            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(ExpectedReferrerUrl, Is.EqualTo(model.ReturnUrl));
        }

        [Test]
        public async Task Then_The_ReturnUrl_Is_Home_If_The_Referrer_Is_Itself()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                    { "Referer", "https://test/Register-interest" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                    HttpContext = _httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = "register-interest"
                    },
                    RouteData = new RouteData()
                }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(ExpectedDefaultUrl, Is.EqualTo(model.ReturnUrl));
        }

        [Test]
        public async Task Then_The_ReturnUrl_Is_Home_If_There_Is_No_Referrer()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers).Returns(new HeaderDictionary([]));
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = { 
                    HttpContext = _httpContext.Object,
                    RouteData = new RouteData() 
                }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.Null);
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(ExpectedDefaultUrl, Is.EqualTo(model.ReturnUrl));
            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("Index") && c.Controller.Equals("Home"))));
        }

        [Test]
        public async Task Then_When_Viewing_The_RegisterInterest_Form_The_Controller_And_Action_Segments_Are_Used()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                     { "Referer", "https://test2/some-controller/some/" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     ActionDescriptor = new ControllerActionDescriptor
                     {
                         ControllerName = "register-interest"
                     },
                     RouteData = new RouteData()
                 }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));


            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("some") && c.Controller.Equals("some-controller"))));
        }

        [Test]
        public async Task Then_When_Viewing_The_RegisterInterest_Form_Then_If_There_Are_No_Controller_Action_Segments_Default_Values_AreUsed()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                     { "Referer", "https://test2/" }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     ActionDescriptor = new ControllerActionDescriptor
                     {
                         ControllerName = "register-interest"
                     },
                     RouteData = new RouteData()
                 }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));

            Assert.That(ExpectedDefaultUrl, Is.EqualTo(model.ReturnUrl));
            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("Index") && c.Controller.Equals("Home"))));
        }

        [Test]
        public async Task When_clicking_register_interest_from_a_search_result_page_then_query_string_is_appended()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();

            var ExpectedRefererUrlWithQuerystring = $"{ExpectedDefaultUrl}?Postcode=CV1+2WT";

            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                     { "Referer", ExpectedRefererUrlWithQuerystring }
                }));

            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     ActionDescriptor = new ControllerActionDescriptor
                     {
                         ControllerName = "register-interest"
                     },
                     RouteData = new RouteData()
                 }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));

            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));

            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(ExpectedRefererUrlWithQuerystring, Is.EqualTo(model.ReturnUrl));
        }

        [Test]
        public async Task Then_StoreData_Is_Called_On_The_UserDataCollection_Service()
        {
            //Act
            await _controller.Index(_registerInterestModel);

            //Assert
            _userDataCollection.Verify(x => x.StoreUserData(It.Is<UserData>(
                c => c.Email.Equals(_registerInterestModel.Email) &&
                   c.FirstName.Equals(_registerInterestModel.FirstName) &&
                   c.LastName.Equals(_registerInterestModel.LastName) &&
                   c.RouteId.Equals(((int)_registerInterestModel.Route).ToString()) &&
                   c.CookieId.Equals(ExpectedCookieId)
                )), Times.Once);
        }

        [Test]
        public async Task Then_If_The_Ga_Cookie_Is_Not_Available_A_Default_Value_Is_Used()
        {
            //Arrange
            _httpContext = new Mock<HttpContext>();

            _httpContext.Setup(x => x.Request.Cookies["_ga"]).Returns(string.Empty);
            _httpContext.Setup(x => x.Request.Path).Returns("/");

            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     RouteData = new RouteData()
                 }
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
            _controller.ModelState.AddModelError("FirstName", "First name");

            //Act
            await _controller.Index(_registerInterestModel);

            //Assert
            _userDataCollection.Verify(x => x.StoreUserData(It.IsAny<UserData>()), Times.Never);
        }

        [Test]
        public async Task Then_If_The_Model_Is_Not_Valid_Then_The_Menu_Is_Added_To_The_ViewModel()
        {
            //Arrange
            _controller.ModelState.AddModelError("FirstName", "First name");

            //Act
            var actual = await _controller.Index(_registerInterestModel) as ViewResult;

            //Assert
            _mediator.Verify(x => x.Send(It.IsAny<GetMenuQuery>(), CancellationToken.None), Times.Once);
            Assert.That(actual, Is.Not.Null);
            var actualModel = actual.Model as RegisterInterestModel;
            Assert.That(actualModel, Is.Not.Null);
            actualModel.Menu.Should().NotBeNull();
        }

        [Test]
        public async Task Then_If_The_Model_Is_Not_Valid_And_Route_Is_Not_Known_Then_Show_Route_Question()
        {
            //Arrange
            _controller.ModelState.AddModelError("FirstName", "First name");

            //Act
            var actual = await _controller.Index(_registerInterestModel);

            Assert.That(actual, Is.Not.Null);
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.ShowRouteQuestion, Is.EqualTo(true));
        }

        [Test]
        public async Task Then_If_The_Model_Is_Not_Valid_And_Route_Is_Known_Then_Dont_Show_Route_Question()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();

            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     ActionDescriptor = new ControllerActionDescriptor
                     {
                         ControllerName = "register-interest"
                     },
                     RouteData = new RouteData( new RouteValueDictionary{ {"route","apprentice" } })
                 }
            };

            _controller.ModelState.AddModelError("FirstName", "First name");

            //Act
            var actual = await _controller.Index(_registerInterestModel);

            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));

            Assert.That(model.ShowRouteQuestion, Is.EqualTo(false));
        }

        [Test]
        public async Task Then_If_The_UserDataCollection_Fails_Validation_The_Errors_Are_Returned_To_The_View()
        {
            //Arrange
            _registerInterestModel.Email = "a@a";
            _userDataCollection.Setup(x => x.StoreUserData(It.IsAny<UserData>()))
                               .ThrowsAsync(new ValidationException(new ValidationResult("Failed", ["Email|The Email field is not valid."]), null, null));

            //Act
            var actual = await _controller.Index(_registerInterestModel);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var actualViewResult = actual as ViewResult;
            Assert.That(actualViewResult, Is.Not.EqualTo(null));
            Assert.That(actualViewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(actualViewResult.ViewData.ModelState.ContainsKey("Email"), Is.EqualTo(true));
            var model = actualViewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));
            Assert.That(model.Menu, Is.Not.EqualTo(null));
        }

        [Test]
        public async Task When_Referring_From_An_FAA_Vacancy_Search_The_Full_Url_Is_Returned()
        {
            //Arrange
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                         .Returns(ExpectedDefaultUrl).Verifiable();
            _httpContext.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                     { "Referer", ExpectedVacancySearchReferrerURl }
                }));
            _controller = new RegisterInterestController(_userDataCollection.Object, _mediator.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = {
                     HttpContext = _httpContext.Object,
                     ActionDescriptor = new ControllerActionDescriptor
                     {
                         ControllerName = "register-interest"
                     },
                     RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                 }
            };

            //Act
            var actual = await _controller.Index(RouteType.Apprentice);

            //Assert
            Assert.That(actual, Is.Not.EqualTo(null));
            var viewResult = actual as ViewResult;
            Assert.That(viewResult, Is.Not.EqualTo(null));
            var model = viewResult.Model as RegisterInterestModel;
            Assert.That(model, Is.Not.EqualTo(null));

            Assert.That(ExpectedDefaultUrl, Is.EqualTo(model.ReturnUrl));
            mockUrlHelper.Verify(x => x.Action(It.Is<UrlActionContext>(c => c.Action.Equals("SearchResults/standard/postcode/distance") && c.Controller.Equals("cpg"))));
        }
    }
}

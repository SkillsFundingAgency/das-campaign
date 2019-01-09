using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
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
            _httpContext = new Mock<HttpContext>();
            _httpContext.Setup(x => x.Request.Cookies).Returns(cookies);

            _controller = new RegisterInterestController(_userDataCollection.Object)
            {
                ControllerContext = {HttpContext = _httpContext.Object}
            };
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

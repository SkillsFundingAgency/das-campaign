using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.DataCollection.Validation;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection.UserDataCollectionTests
{
    public class WhenValidatingUserData
    {
        private UserDataCollectionValidator _validator;

        [SetUp]
        public void Arrange()
        {
            _validator = new UserDataCollectionValidator();
        }

        [TestCase("test@test.com","a","b","1","")]
        [TestCase("test@test.com", "a", "b", "", "2")]
        [TestCase("test@test.com", "a", "", "1", "2")]
        [TestCase("test@test.com", "", "b", "1", "2")]
        [TestCase("", "a", "b", "1", "2")]
        [TestCase(" ", " ", " ", " ", " ")]
        [TestCase("", "", "", "", "")]
        public void Then_If_There_Are_Missing_Values_False_Is_Returned(string email, string firstName, string lastName, string routeId, string cookieId)
        {
            //Act
            var actual = _validator.Validate(new UserData());

            //Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void Then_True_Is_Returned_If_The_Model_Is_Correctly_Populated()
        {
            //Act
            var actual = _validator.Validate(new UserData
            {
                Email = "a'a@a.com",
                FirstName = "test",
                LastName = "tester",
                RouteId = "123",
                CookieId = "54321"
            });

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Then_False_Is_Returned_If_The_Email_Is_Not_Valid()
        {
            //Act
            var actual = _validator.Validate(new UserData
            {
                Email = "a",
                FirstName = "test",
                LastName = "tester",
                RouteId = "123",
                CookieId = "54321"
            });

            //Assert
            Assert.IsFalse(actual);
        }

        [TestCase("test@test.com", true)]
        [TestCase("test@", false)]
        [TestCase("", false)]
        public void Then_The_Email_Is_Validated(string email, bool expectedResult)
        {
            //Act
            var actual = _validator.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
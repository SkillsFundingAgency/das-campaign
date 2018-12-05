using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.DataCollection.Services;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection.Services
{
    public class WhenUsingTheUrlUserService
    {
        private UserDataCryptographyService _userDataCryptographyService;
        private Mock<IOptions<CampaignConfiguration>> _options;


        [SetUp]
        public void Arrange()
        {
            _options = new Mock<IOptions<CampaignConfiguration>>();
            _options.Setup(x => x.Value.UserUrlSalt).Returns("Test Value");
            _options.Setup(x => x.Value.UserUrlMinValue).Returns(10);
            _options.Setup(x => x.Value.AllowedUrlCharacters).Returns("ABCDEFHJKLMNPRSTUV34689");

            _userDataCryptographyService = new UserDataCryptographyService(_options.Object);
        }

        [Test]
        public void Then_The_Configuration_Options_Are_Used_For_Generating_The_Encoded_Url()
        {
            //Act
            _userDataCryptographyService.GenerateEncodedUserId(12345);

            //Assert
            _options.Verify(x=>x.Value.UserUrlMinValue, Times.Once);
            _options.Verify(x=>x.Value.UserUrlSalt, Times.Once);
            _options.Verify(x=>x.Value.AllowedUrlCharacters, Times.Once);
        }

        [Test]
        public void Then_The_Encoded_String_Contains_The_UserId()
        {
            //Arrange
            var userId = 12345;

            //Act
            var actual = _userDataCryptographyService.GenerateEncodedUserId(userId);

            //Assert
            Assert.AreNotEqual(string.Empty, actual);
            Assert.AreNotEqual(userId, actual);
        }

        [Test]
        public void Then_The_Encoded_String_Can_Be_Decoded_To_Get_The_User_Id()
        {
            //Arrange
            var userId = 12345;

            //Act
            var encoded = _userDataCryptographyService.GenerateEncodedUserId(userId);
            var actual = _userDataCryptographyService.DecodeUserId(encoded);

            //Assert
            Assert.AreEqual(userId, actual);
        }

        [Test]
        public void Then_If_An_Invalid_Encoded_Value_Is_Passed_Then_Zero_Is_Returned()
        {
            //Act
            var actual = _userDataCryptographyService.DecodeUserId("ZZ2211ASD");

            //Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Then_The_Encoded_String_Can_Be_Decoded_To_Get_The_User_Ids()
        {
            //Arrange
            var userId = 12345;

            //Act
            var encoded = _userDataCryptographyService.GenerateEncodedUserId(userId);
            var actual = _userDataCryptographyService.DecodeUserId(encoded);

            //Assert
            Assert.AreEqual(userId, actual);
        }
    }
}

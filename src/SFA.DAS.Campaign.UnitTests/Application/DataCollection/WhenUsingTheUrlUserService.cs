using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Application.UnitTests.DataCollection
{
    public class WhenUsingTheUrlUserService
    {
        private UserDataCryptographyService _userDataCryptographyService;
        private Mock<IOptions<UserDataCryptography>> _options;


        [SetUp]
        public void Arrange()
        {
            _options = new Mock<IOptions<UserDataCryptography>>();
            _options.Setup(x => x.Value.UserUrlSalt).Returns("Test Value");
            _options.Setup(x => x.Value.UserUrlMinValue).Returns(10);
            _options.Setup(x => x.Value.AllowedUrlCharacters).Returns("ABCDEFHJKLMNPRSTUV34689");

            _userDataCryptographyService = new UserDataCryptographyService(_options.Object);
        }

        [Test]
        public void Then_The_Configuration_Options_Are_Used_For_Generating_The_Encoded_Url()
        {
            //Act
            _userDataCryptographyService.GenerateEncodedUserEmail("te'st@test.local");

            //Assert
            _options.Verify(x=>x.Value.UserUrlMinValue, Times.Once);
            _options.Verify(x=>x.Value.UserUrlSalt, Times.Once);
            _options.Verify(x=>x.Value.AllowedUrlCharacters, Times.Once);
        }

        [Test]
        public void Then_The_Encoded_String_Contains_The_User_Email()
        {
            //Arrange
            var userId = "te'st@test.local";

            //Act
            var actual = _userDataCryptographyService.GenerateEncodedUserEmail(userId);

            //Assert
            Assert.That(string.Empty, Is.Not.EqualTo(actual));
            Assert.That(userId, Is.Not.EqualTo(actual));

        }

        [Test]
        public void Then_The_Encoded_String_Can_Be_Decoded_To_Get_The_User_Email()
        {
            //Arrange
            var userId = "te'st@test.local";

            //Act
            var encoded = _userDataCryptographyService.GenerateEncodedUserEmail(userId);
            var actual = _userDataCryptographyService.DecodeUserEmail(encoded);

            //Assert
            Assert.That(userId, Is.EqualTo(actual));
        }

        [Test]
        public void Then_If_An_Invalid_Encoded_Value_Is_Passed_Then_An_Empty_String_Is_Returned()
        {
            //Act
            var actual = _userDataCryptographyService.DecodeUserEmail("AA99BBCCDDEEFF");

            //Assert
            Assert.That("", Is.EqualTo(actual));
        }

    }
}

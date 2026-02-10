using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application;
using SFA.DAS.Campaign.Application.DataCollection;
using SFA.DAS.Campaign.Application.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ValidationResult = SFA.DAS.Campaign.Application.ValidationResult;

namespace SFA.DAS.Campaign.UnitTests.Application.DataCollection.UserDataCollectionTests;

public class WhenCollectingUserData
{
    private UserData _userData;
    private Mock<IUserDataCollectionValidator> _userDataCollectionValidator;
    private UserDataCollection _userDataCollection;
    private Mock<IExternalApiService> _externalApiService;
    private ILogger<UserDataCollection> _logger;

    [SetUp]
    public void Arrange()
    {
        _userData = new UserData();
        _externalApiService = new Mock<IExternalApiService>();
        _logger = new Mock<ILogger<UserDataCollection>>().Object;
        _userDataCollectionValidator = new Mock<IUserDataCollectionValidator>();
        _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(new ValidationResult());
        _userDataCollection = new UserDataCollection(_userDataCollectionValidator.Object, _logger, _externalApiService.Object);
    }

    [Test]
    public async Task Then_The_Parameters_Are_Validated()
    {
        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Arrange
        _userDataCollectionValidator.Verify(x => x.Validate(_userData), Times.Once);
    }

    [Test]
    public async Task Then_If_The_Validation_Is_Successful_ExternalApiService_Is_Called()
    {
        //Arrange
        _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Assert
        _externalApiService.Verify(x => x.PostDataAsync("RegisterInterest", It.IsAny<UserDataDto>()), Times.Once);
    }

    [Test]
    public void Then_If_The_Validation_Fails_ExternalApiService_Is_Not_Called_And_An_Exception_Is_Thrown()
    {
        //Arrange
        var result = new ValidationResult();
        result.AddError("test", ValidationFailure.NotPopulated);
        _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(result);

        //Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () => await _userDataCollection.StoreUserData(new UserData()));
        _externalApiService.Verify(x => x.PostDataAsync(It.IsAny<string>(), It.IsAny<UserDataDto>()), Times.Never);
    }

    [Test]
    public async Task Then_If_The_Validation_Is_Successful_An_Exception_Is_Not_Thrown()
    {
        //Arrange
        _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

        //Act/Assert
        Assert.DoesNotThrowAsync(async () => await _userDataCollection.StoreUserData(_userData));
    }

    [Test]
    public async Task Then_If_An_Exception_Is_Thrown_It_Is_Caught_And_Logged()
    {
        //Arrange
        var exception = new System.Exception("Test exception");
        _externalApiService.Setup(x => x.PostDataAsync(It.IsAny<string>(), It.IsAny<UserDataDto>())).ThrowsAsync(exception);
        var loggerMock = new Mock<ILogger<UserDataCollection>>();
        _userDataCollection = new UserDataCollection(_userDataCollectionValidator.Object, loggerMock.Object, _externalApiService.Object);

        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Assert
        loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred while logging validation success")),
            exception,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Test]
    public async Task Then_If_The_Validation_Fails_The_UserData_Is_Not_Passed_To_The_ExternalApiService()
    {
        //Arrange
        var result = new ValidationResult();
        result.AddError("test", ValidationFailure.NotPopulated);
        _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(result);

        //Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () => await _userDataCollection.StoreUserData(new UserData()));
        _externalApiService.Verify(x => x.PostDataAsync(It.IsAny<string>(), It.IsAny<UserDataDto>()), Times.Never);
    }

    [Test]
    public async Task Then_If_The_Validation_Fails_An_Exception_Is_Thrown()
    {
        //Arrange
        var result = new ValidationResult();
        result.AddError("test", ValidationFailure.NotPopulated);
        _userDataCollectionValidator.Setup(x => x.Validate(It.IsAny<UserData>())).Returns(result);

        //Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () => await _userDataCollection.StoreUserData(new UserData()));
    }

    [Test]
    public async Task Then_If_The_Validation_Is_Successful_The_UserData_Is_Passed_To_The_ExternalApiService()
    {
        //Arrange
        _userData = new UserData
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };
        _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Assert
        _externalApiService.Verify(x => x.PostDataAsync("RegisterInterest", It.IsAny<UserDataDto>()), Times.Once);
    }

    [Test]
    public async Task Then_If_The_Validation_Is_Successful_The_UserData_Is_Passed_To_The_ExternalApiService_With_CapturedDto()
    {
        //Arrange
        _userData = new UserData
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

        UserDataDto capturedUserDataDto = null;

        _externalApiService.Setup(x => x.PostDataAsync("RegisterInterest", It.IsAny<object>()))
            .Callback<string, object>((endpoint, body) => capturedUserDataDto = body as UserDataDto)
            .ReturnsAsync(string.Empty);

        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Assert
        Assert.That(capturedUserDataDto, Is.Not.Null);
        Assert.That(capturedUserDataDto.FirstName, Is.EqualTo(_userData.FirstName));
        Assert.That(capturedUserDataDto.LastName, Is.EqualTo(_userData.LastName));
        Assert.That(capturedUserDataDto.Email, Is.EqualTo(_userData.Email));
    }

    [Test]
    public async Task Then_If_The_Validation_Is_Successful_The_UserData_Is_Passed_To_The_ExternalApiService_With_CorrectEndpoint()
    {
        //Arrange
        _userData = new UserData
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        _userDataCollectionValidator.Setup(x => x.Validate(_userData)).Returns(new ValidationResult());

        string capturedEndpoint = null;

        _externalApiService.Setup(x => x.PostDataAsync(It.IsAny<string>(), It.IsAny<object>()))
            .Callback<string, object>((endpoint, body) => capturedEndpoint = endpoint)
            .ReturnsAsync(string.Empty);

        //Act
        await _userDataCollection.StoreUserData(_userData);

        //Assert
        Assert.That(capturedEndpoint, Is.EqualTo("RegisterInterest"));
    }

}

using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using FluentAssertions;
using SFA.DAS.Campaign.Web.Validators;

namespace SFA.DAS.Campaign.UnitTests.Web.Validators
{
    public class WhenValidatingRolesAttribute
    {
        private RolesValidationAttribute _attribute;

        [SetUp]
        public void Arrange()
        {
            _attribute = new RolesValidationAttribute(1);
        }

        [TestCase(null, "Enter how many roles you have available for this apprenticeship")]
        [TestCase("", "Enter how many roles you have available for this apprenticeship")]
        [TestCase(" ", "Enter how many roles you have available for this apprenticeship")]
        public void Then_Returns_Error_When_Value_Is_NullOrEmpty(string input, string expectedMessage)
        {
            var result = _attribute.GetValidationResult(input, new ValidationContext(new { }));

            result.ErrorMessage.Should().Be(expectedMessage);
        }

        [TestCase("abc", "Number of roles available for this apprenticeship must be a whole number")]
        [TestCase("3.5", "Number of roles available for this apprenticeship must be a whole number")]
        [TestCase("-1", "Number of roles available for this apprenticeship must be a whole number")]
        public void Then_Returns_Error_When_Value_Is_Not_A_Whole_Number(string input, string expectedMessage)
        {
            var result = _attribute.GetValidationResult(input, new ValidationContext(new { }));

            result.ErrorMessage.Should().Be(expectedMessage);
        }

        [TestCase("0", "Number of roles available for this apprenticeship must be 1 or more")]
        [TestCase("1", null)]
        [TestCase("5", null)]
        public void Then_Returns_Error_When_Value_Is_Less_Than_Minimum(string input, string expectedMessage)
        {
            var result = _attribute.GetValidationResult(input, new ValidationContext(new { }));

            if (expectedMessage == null)
            {
                result.Should().BeNull();
            }
            else
            {
                result.ErrorMessage.Should().Be(expectedMessage);
            }
        }

        [TestCase("1", null)]
        [TestCase("2", null)]
        [TestCase("10", null)]
        public void Then_Returns_Valid_When_Value_Is_Whole_And_Above_Minimum(string input, string expectedMessage)
        {
            var result = _attribute.GetValidationResult(input, new ValidationContext(new { }));

            result.Should().BeNull();
        }
    }
}

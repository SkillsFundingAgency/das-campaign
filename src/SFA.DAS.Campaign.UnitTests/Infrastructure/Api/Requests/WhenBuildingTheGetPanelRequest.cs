using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Testing.AutoFixture;
using System.Web;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheGetPanelRequest
    {
        [Test, MoqAutoData]
        public void Then_The_Url_Is_Correct(int panelId)
        {
            var actual = new GetPanelRequest(panelId);

            actual.GetUrl.Should().Be($"panel/{panelId}");
        }
    }
}

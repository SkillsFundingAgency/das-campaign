using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public interface IHtmlControlAbstractFactory
    {
        IHtmlControlFactory CreateControlFactoryFor(Item item);
    }
}

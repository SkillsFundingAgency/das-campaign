using System.Text;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public interface IHtmlControlAbstractFactory
    {
        IHtmlControlFactory CreateControlFactoryFor(Item item);
    }
}

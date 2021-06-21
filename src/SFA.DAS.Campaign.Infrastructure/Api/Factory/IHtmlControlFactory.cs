using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public interface IHtmlControlFactory
    {
        IHtmlControl Create(Item control);

        bool IsValid(Item control);
    }
}

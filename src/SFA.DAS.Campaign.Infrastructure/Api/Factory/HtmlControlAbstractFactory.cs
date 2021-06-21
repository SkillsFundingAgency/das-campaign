using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class HtmlControlAbstractFactory : IHtmlControlAbstractFactory
    {
        private readonly IEnumerable<IHtmlControlFactory> _factories;

        public HtmlControlAbstractFactory(IEnumerable<IHtmlControlFactory> factories)
        {
            _factories = factories;
        }

        public IHtmlControlFactory CreateControlFactoryFor(Item item)
        {
            return _factories.FirstOrDefault(factory => factory.IsValid(item));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders
{
    public class ItemBuilder
    {
        private readonly Item _item;

        public ItemBuilder()
        {
            _item = new Item
            {
                TableValue = new List<List<string>>(),
            };
        }

        public static ItemBuilder New()
        {
            return new ItemBuilder();
        }

        public ItemBuilder SetType(string type)
        {
            _item.Type = type;

            return this;
        }

        public ItemBuilder SetValues(List<string> values)
        {
            _item.Values = values;

            return this;
        }

        public ItemBuilder SetValue(string value)
        {
            _item.Values.Add(value);

            return this;
        }

        public ItemBuilder SetTableValue(List<string> tableValues)
        {
            _item.TableValue.Add(tableValues);

            return this;
        }

        public ItemBuilder AddEmbeddedResource(EmbeddedResource resource)
        {
            _item.EmbeddedResource = resource;

            return this;
        }

        public Item Build()
        {
            return _item;
        }
    }
}

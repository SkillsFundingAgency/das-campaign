using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class TableControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var table = new Table();

            table.Headings.AddRange(control.TableValue[0]);

            for (int i = 1; i < control.TableValue.Count; i++)
            {
                table.Rows.AddRange(control.TableValue[i]);
            }

            return table;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0 && control.TableValue.Any())
            {
                return true;
            }

            return false;
        }
    }
}

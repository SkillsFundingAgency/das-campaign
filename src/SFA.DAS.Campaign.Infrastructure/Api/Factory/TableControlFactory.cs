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
            var hasHeaderRow = !IsNativeTable(control) || control.TableHasHeaderRow;

            var table = new Table
            {
                HasHeaderColumn = control.TableHasHeaderColumn,
                ColumnCount = control.TableValue.Max(row => row.Count)
            };

            if (hasHeaderRow)
            {
                table.Headings.AddRange(control.TableValue[0]);
            }

            for (int i = hasHeaderRow ? 1 : 0; i < control.TableValue.Count; i++)
            {
                table.Rows.AddRange(control.TableValue[i]);
            }

            return table;
        }

        public bool IsValid(Item control)
        {
            if (control.TableValue == null || !control.TableValue.Any())
            {
                return false;
            }

            return IsNativeTable(control)
                   || string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0;
        }

        private static bool IsNativeTable(Item control)
        {
            return string.Compare(control.Type, "table", StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}

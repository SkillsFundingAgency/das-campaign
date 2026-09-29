using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class TableControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is Table;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Table;

            var para = new TagBuilder("table");
            para.AddCssClass("fiu-table");
            
            AddHeadingElements(para, control);
            AddRowElements(para, control);

            string result = para.WriteString();

            return new HtmlString(result);
        }

        private static void AddHeadingElements(TagBuilder para, Table control)
        {
            if (control.Headings.Count == 0)
            {
                return;
            }

            para.InnerHtml.AppendHtml("<thead><tr>");

            foreach (var value in control.Headings)
            {
                para.InnerHtml.AppendHtml($"<th scope=\"col\">{value.CheckForFontEffects().CheckForAndConstructHyperlinks().LineBreaksToHtml()}</th>");
            }

            para.InnerHtml.AppendHtml("</tr></thead>");
        }

        private static void AddRowElements(TagBuilder para, Table control)
        {
            para.InnerHtml.AppendHtml("<tbody>");

            var column = 0;

            foreach (var value in control.Rows)
            {
                if (column == 0)
                {
                    para.InnerHtml.AppendHtml($"<tr>");
                }

                var cellContent = (value ?? string.Empty).CheckForFontEffects().CheckForAndConstructHyperlinks().LineBreaksToHtml();

                if (column == 0 && control.HasHeaderColumn)
                {
                    para.InnerHtml.AppendHtml($"<th scope=\"row\">{cellContent}</th>");
                }
                else
                {
                    para.InnerHtml.AppendHtml($"<td>{cellContent}</td>");
                }

                column += 1;

                if (column >= control.ColumnCount)
                {
                    para.InnerHtml.AppendHtml($"</tr>");
                    column = 0;
                }
            }

            para.InnerHtml.AppendHtml("</tbody>");
        }
    }
}

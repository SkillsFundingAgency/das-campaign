using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Hub : IContentType
    {
        public Hub()
        {
            Cards = new List<ArticleRelated>();
        }
        public Image HeaderImage { get; set; }
        public List<ArticleRelated> Cards { get; set; }
    }
}

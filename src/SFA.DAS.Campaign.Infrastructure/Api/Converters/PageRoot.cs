using System;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class PageRoot
    {
        public ResponseArticle Article { get; set; }
        public ResponseHub Hub { get; set; }
        public ResponseHub LandingPage { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class PageRoot
    {
        public ResponseArticle Article { get; set; }
        public ResponseHub Hub { get; set; }
        public ResponseHub LandingPage { get; set; }
        public ResponseSiteMap Map { get; set; }
        public ResponseMenu Menu { get; set; }
        public ResponseBanner Banner { get; set; }
        public ResponsePanel Panel { get; set; }
        
    }
}

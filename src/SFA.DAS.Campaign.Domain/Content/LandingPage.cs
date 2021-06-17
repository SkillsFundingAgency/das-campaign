using System;
using System.Collections.Generic;
using System.Text;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class LandingPage
    {
        public string Title { get; set; }
        public Document Body { get; set; }
    }
}

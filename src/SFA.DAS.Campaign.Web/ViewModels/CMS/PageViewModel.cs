using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core.Models;
using SFA.DAS.Campaign.Web.Models.CMS;

namespace SFA.DAS.Campaign.Web.ViewModels.CMS
{
    public class PageViewModel
    {
        public IEnumerable<SystemProperties> SystemProperties { get; set; }
        public Page Page { get; set; }
    }
}

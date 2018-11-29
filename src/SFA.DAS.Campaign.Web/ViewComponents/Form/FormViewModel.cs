using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.ViewComponents.Form
{
    public class FormViewModel
    {
        public HttpMethod ResponseType { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

    }
}

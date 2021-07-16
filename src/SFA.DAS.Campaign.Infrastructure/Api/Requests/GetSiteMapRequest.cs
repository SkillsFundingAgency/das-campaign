using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetSiteMapRequest : IGetApiRequest
    {
        public GetSiteMapRequest()
        {
        }

        public string GetUrl => $"sitemap";
    }
}

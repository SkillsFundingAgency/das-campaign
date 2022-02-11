using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Api.Interfaces;

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

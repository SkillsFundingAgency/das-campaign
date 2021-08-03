using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetBannerRequest : IGetApiRequest
    {
        public string GetUrl => $"banner";
    }
}

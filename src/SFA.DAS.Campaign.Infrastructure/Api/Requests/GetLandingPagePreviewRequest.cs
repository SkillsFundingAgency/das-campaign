using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetLandingPagePreviewRequest : IGetApiRequest
    {
        private readonly string _hub;
        private readonly string _slug;

        public GetLandingPagePreviewRequest(string hub, string slug)
        {
            _hub = hub;
            _slug = slug;
        }

        public string GetUrl => $"landingpage/preview/{_hub}/{_slug}";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetArticlesPreviewRequest : IGetApiRequest
    {
        private readonly string _hub;
        private readonly string _slug;

        public GetArticlesPreviewRequest(string hub, string slug)
        {
            _hub = hub;
            _slug = slug;
        }

        public string GetUrl => $"article/preview/{_hub}/{_slug}";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetArticlesRequest : IGetApiRequest
    {
        private readonly string _hub;
        private readonly string _slug;
        private readonly bool _local;

        public GetArticlesRequest(string hub, string slug, bool local)
        {
            _hub = hub;
            _slug = slug;
            _local = local;
        }

        public string GetUrl => $"article/{_hub}/{_slug}";
    }
}

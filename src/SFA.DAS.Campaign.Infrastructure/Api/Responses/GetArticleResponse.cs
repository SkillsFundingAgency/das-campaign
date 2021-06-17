using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetArticleResponse
    {
        public Page<Article> Article { get; set; }
    }
}

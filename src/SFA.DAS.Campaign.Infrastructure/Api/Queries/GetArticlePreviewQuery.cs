using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetArticlePreviewQuery : IRequest<GetArticlePreviewQueryResult<Domain.Content.Article>>
    {
        public string Hub { get; set; }
        public string Slug { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, GetArticleQueryResult<Article>>
    {
        public Task<GetArticleQueryResult<Article>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

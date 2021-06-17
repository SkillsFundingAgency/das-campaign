using System;
using System.Collections.Generic;
using System.Text;
using Contentful.Core.Models;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetArticleQueryResult<T> where T: IContentType
    {
        public Page<T> Page { get; set; }
    }
}

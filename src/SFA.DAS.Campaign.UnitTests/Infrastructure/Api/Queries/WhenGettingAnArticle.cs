using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAnArticle
    {
        [Test]
        public async Task ThenTheApiIsCalledWithTheValidRequestParametersAndThe4ArticleIsReturned(
            GetArticleQuery query, Page<Article> response)
        {

        }
    }
}

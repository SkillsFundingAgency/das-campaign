using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAnArticle
    {
        [Test, RecursiveMoqAutoData]
        public async Task ThenTheApiIsCalledWithTheValidRequestParametersAndTheArticleIsReturned(
            GetArticleQuery query, Page<Article> response, [Frozen]Mock<IApiClient> client, [Greedy] GetArticleQueryHandler handler)
        {
            client.Setup(o => o.Get<Page<Article>>(It.IsAny<GetArticlesRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndTheApiIsCalledWithInvalidRequestParametersThenNoArticleIsReturned(
            GetArticleQuery query, [Frozen] Mock<IApiClient> client, [Greedy] GetArticleQueryHandler handler)
        {
            client.Setup(o => o.Get<Page<Article>>(It.IsAny<GetArticlesRequest>())).ReturnsAsync((Page<Article>) null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }
    }
}

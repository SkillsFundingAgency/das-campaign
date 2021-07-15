using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public static class QueryHelpers
    {
        public static async Task FetchMenu<T>(this Page<T> article, IApiClient apiClient) where T : IContentType
        {
            if (article != null)
            {
                var menu = await apiClient.Get<Page<Menu>>(new GetMenuRequest()).ConfigureAwait(false);
                article.Menu = menu.Menu;
            }
        }
    }
}

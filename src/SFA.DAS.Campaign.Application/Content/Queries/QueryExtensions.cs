﻿using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.Application.Content.Queries
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

        public static async Task FetchBanners<T>(this Page<T> article, IApiClient apiClient) where T : IContentType
        {
            if (article != null)
            {
                var menu = await apiClient.Get<Page<BannerContentType>>(new GetBannerRequest()).ConfigureAwait(false);
                article.BannerModels = menu.BannerModels;
            }
        }
    }
}

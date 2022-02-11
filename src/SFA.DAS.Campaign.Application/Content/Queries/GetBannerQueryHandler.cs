using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetBannerQueryHandler : IRequestHandler<GetBannerQuery, GetBannerQueryResult<BannerContentType>>
    {
        private readonly IApiClient _apiClient;

        public GetBannerQueryHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetBannerQueryResult<BannerContentType>> Handle(GetBannerQuery request, CancellationToken cancellationToken)
        {
            var banners = new Page<BannerContentType>();

            await banners.FetchBanners(_apiClient);

            return new GetBannerQueryResult<BannerContentType>
            {
                Page = banners
            };
        }
    }
}

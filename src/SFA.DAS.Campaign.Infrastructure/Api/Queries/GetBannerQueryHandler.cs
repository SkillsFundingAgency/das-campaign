using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
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

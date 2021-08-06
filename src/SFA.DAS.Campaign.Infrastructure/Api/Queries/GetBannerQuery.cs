using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetBannerQuery : IRequest<GetBannerQueryResult<BannerContentType>>
    {
    }
}

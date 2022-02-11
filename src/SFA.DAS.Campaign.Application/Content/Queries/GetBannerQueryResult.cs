﻿using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetBannerQueryResult<T> where T: IContentType
    {
        public Page<T> Page { get; set; }
    }
}

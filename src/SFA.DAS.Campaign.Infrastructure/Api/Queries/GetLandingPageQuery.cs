﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetLandingPageQuery : IRequest<GetLandingPageQueryResult<LandingPage>>
    {
        public string Hub { get; set; }
        public string Slug { get; set; }
        public bool Preview { get; set; }
    }
}

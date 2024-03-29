﻿using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetHubRequest : IGetApiRequest
    {
        private readonly string _hub;

        public GetHubRequest(string hub)
        {
            _hub = hub;
        }

        public string GetUrl => $"hub/{_hub}";
    }
}

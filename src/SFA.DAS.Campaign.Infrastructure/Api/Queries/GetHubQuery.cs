using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetHubQuery : IRequest<GetHubQueryResult<Hub>>
    {
        public string Hub { get; set; }
        public bool Preview { get ; set ; }
    }
}

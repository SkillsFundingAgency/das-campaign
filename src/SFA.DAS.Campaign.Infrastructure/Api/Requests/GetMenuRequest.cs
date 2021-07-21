using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetMenuRequest : IGetApiRequest
    {
        public string GetUrl => $"menu";
    }
}

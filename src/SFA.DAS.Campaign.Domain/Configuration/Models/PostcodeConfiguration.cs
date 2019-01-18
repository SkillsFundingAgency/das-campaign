using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Configuration.Models
{
    public class PostcodeApiConfiguration : IPostcodeApiConfiguration
    {
        public string Url { get; set; }
    }
}

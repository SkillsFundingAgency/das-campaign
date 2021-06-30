using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class EmbeddedResource
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
    }
}

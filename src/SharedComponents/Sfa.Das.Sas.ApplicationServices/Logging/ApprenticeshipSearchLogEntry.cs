using System.Collections.Generic;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.ApplicationServices.Logging
{
    public class ApprenticeshipSearchLogEntry : ILogEntry
    {
        public IEnumerable<string> Keywords { get; set; }

        public long TotalHits { get; set; }

        public string Postcode { get; set; }

        public double[] Coordinates { get; set; }
    }
}

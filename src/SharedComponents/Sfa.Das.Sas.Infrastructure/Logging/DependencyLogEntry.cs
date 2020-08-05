

using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Infrastructure.Logging
{
    public class DependencyLogEntry : ILogEntry
    {
        public string Identifier { get; set; }
        public double ResponseTime { get; set; }
        public int? ResponseCode { get; set; }
        public string Url { get; set; }
    }
}

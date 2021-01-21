using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.ApplicationServices.Logging
{
    public class HttpErrorLogEntry : ILogEntry
    {
        public string Url { get; set; }
    }
}
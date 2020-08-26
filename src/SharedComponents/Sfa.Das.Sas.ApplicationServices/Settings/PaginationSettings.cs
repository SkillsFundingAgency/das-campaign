using System.Configuration;

namespace Sfa.Das.Sas.ApplicationServices.Settings
{
    public class PaginationSettings : IPaginationSettings
    {
        public int DefaultResultsAmount => int.Parse(ConfigurationManager.AppSettings["DefaultResultsAmount"]);
    }
}

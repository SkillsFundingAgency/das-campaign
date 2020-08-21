using StructureMap;

namespace Sfa.Das.Sas.Core.DependencyResolution
{
    public sealed class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            For<IRetryWebRequests>().Use<WebRequestRetryService>();
        }
    }
}

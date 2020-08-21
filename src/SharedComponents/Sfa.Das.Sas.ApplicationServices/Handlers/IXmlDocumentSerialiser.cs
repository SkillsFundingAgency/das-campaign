using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public interface IXmlDocumentSerialiser
    {
        string Serialise(string xmlNamespace, string urlPlaceholder, IEnumerable<string> items);
    }
}
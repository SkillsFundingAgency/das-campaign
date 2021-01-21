using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class XmlDocumentSerialiser : IXmlDocumentSerialiser
    {
        public string Serialise(string xmlNamespace, string urlPlaceholder, IEnumerable<string> items)
        {
            XNamespace ns = xmlNamespace;

            XElement urlSetElement;

            if (items != null && items.Any())
            {
                urlSetElement = new XElement(ns + "urlset", items.Select(id => new XElement(ns + "url", new XElement(ns + "loc", string.Format(urlPlaceholder, id)))));
            }
            else
            {
                urlSetElement = new XElement(ns + "urlset");
            }

            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlSetElement);

            return document.ToString();
        }
    }
}
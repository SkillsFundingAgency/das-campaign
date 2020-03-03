using Contentful.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.Campaign.Web.Configuration;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Base model for all other models containing some common configuration.
    /// </summary>
    public class BasePageModel
    {
        /// <summary>
        /// The client used to communicate with the Contentful API.
        /// </summary>
        protected readonly IContentfulClient _client;

        /// <summary>
        /// Initializes a new BasePageModel.
        /// </summary>
        /// <param name="client">The client used to communicate with the Contentful API.</param>
        public BasePageModel(IContentfulClient client)
        {
            _client = client;
            _client.ContentTypeResolver = new ModulesResolver();
        }
    }
}

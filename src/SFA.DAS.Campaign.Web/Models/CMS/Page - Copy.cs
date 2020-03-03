using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    public class PageCopy : IPageModule
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the module.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The body copy of the module.
        /// </summary>
        public string Headline { get; set; }

        public Document Content { get; set; }
    } 
}

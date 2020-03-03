using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a layout.
    /// </summary>
    public class Company : IPageModule
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the layout.
        /// </summary>
        public string Title { get; set; }
        public string Slug { get; set; }
        public string CompanyName { get; set; }
        public Asset Image { get; set; }

        public Document Description { get; set; }


    }
}

using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a layout.
    /// </summary>
    public class SectionCompanies : IPageModule
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the layout.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The slug of the layout.
        /// </summary>
        public int NumberToShow { get; set; }

        /// <summary>
        /// The modules that make up the layout.
        /// </summary>
        public List<IPageModule> RealStories { get; set; }
    }
}

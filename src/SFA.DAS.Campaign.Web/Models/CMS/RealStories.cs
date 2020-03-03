using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a layout.
    /// </summary>
    public class RealStories : IPageModule
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
        public string Headline { get; set; }
        public Asset Image { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public Document ShortContent { get; set; }
        public Document FullContent { get; set; }
    }
}

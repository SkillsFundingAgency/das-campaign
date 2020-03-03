using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a lesson.
    /// </summary>
    public class Sidebar : IPageModule
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the lesson.
        /// </summary>
        public string Title { get; set; }

        public Asset Image { get; set; }

    }
}

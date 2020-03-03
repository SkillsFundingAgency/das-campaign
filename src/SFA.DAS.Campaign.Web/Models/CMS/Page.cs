using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a lesson.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the lesson.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The slug of the lesson.
        /// </summary>
        public string Slug { get; set; }

        public HeroHeading HeroHeading { get; set; }

        /// <summary>
        /// The modules that makes up the lesson.
        /// </summary>
        public List<IPageModule> Modules { get; set; }
    }
}

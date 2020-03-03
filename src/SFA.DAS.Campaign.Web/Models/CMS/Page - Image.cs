using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a image module for a lesson.
    /// </summary>
    public class PageImage : IPageModule
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
        /// The image of the module.
        /// </summary>
        public Asset Image { get; set; }

        /// <summary>
        /// The caption of the image.
        /// </summary>
        public string Caption { get; set; }
    }
}

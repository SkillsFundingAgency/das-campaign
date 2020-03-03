using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }

        /// <summary>
        /// The title of the category.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The slug for the category.
        /// </summary>
        public string Slug { get; set; }
    }
}

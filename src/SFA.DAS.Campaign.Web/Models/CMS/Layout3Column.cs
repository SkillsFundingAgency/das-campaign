using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Web.Models.CMS
{
    /// <summary>
    /// Represents a layout.
    /// </summary>
    public class Layout3Column : IPageModule
    {
        /// <summary>
        /// The system defined meta data properties.
        /// </summary>
        public SystemProperties Sys { get; set; }


        /// <summary>
        /// The modules that make up the layout.
        /// </summary>
        public List<IPageModule> Column1 { get; set; }
        public List<IPageModule> Column2 { get; set; }
        public List<IPageModule> Column3 { get; set; }
    }
}

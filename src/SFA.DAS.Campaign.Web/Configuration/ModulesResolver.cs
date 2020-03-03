using System;
using System.Collections.Generic;
using Contentful.Core.Configuration;
using SFA.DAS.Campaign.Web.Models.CMS;

namespace SFA.DAS.Campaign.Web.Configuration
{
    /// <summary>
    /// Resolves a strong type from a content type id. Instructing the serialization engine how to deserialize items in a collection.
    /// </summary>
    public class ModulesResolver : IContentTypeResolver
    {
        private Dictionary<string, Type> _types = new Dictionary<string, Type>()
        {
            { "pageCopy", typeof(PageCopy) },
            { "pageImage", typeof(PageImage) },
            { "layout", typeof(Layout) },
            { "layout2Column", typeof(Layout2Column) },
            { "layout3Column", typeof(Layout3Column) },
            { "heroHeading", typeof(HeroHeading) },
            { "sectionRealStories", typeof(SectionRealStories) },
            { "realStories", typeof(RealStories) },
            { "sidebar", typeof(Sidebar) },
            { "company", typeof(Company) },
            { "sectionCompanies", typeof(SectionCompanies) },




        };

        /// <summary>
        /// Method to get a type based on the specified content type id.
        /// </summary>
        /// <param name="contentTypeId">The content type id to resolve to a type.</param>
        /// <returns>The type for the content type id or null if none is found.</returns>
        public Type Resolve(string contentTypeId)
        {
            return _types.TryGetValue(contentTypeId, out var type) ? type : null;
        }
    }
}

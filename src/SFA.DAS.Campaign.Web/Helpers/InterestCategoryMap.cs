using System;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public class InterestCategory
    {
        public InterestCategory(int routeId, string categoryName)
        {
            RouteId = routeId;
            CategoryName = categoryName;
        }
        
        public int RouteId { get; }
        public string CategoryName { get; }
    }

    public static class InterestCategoryMap
    {
        private static readonly IReadOnlyDictionary<string, InterestCategory> Categories =
            new Dictionary<string, InterestCategory>(StringComparer.OrdinalIgnoreCase)
            {
                ["Agriculture, environmental and animal care"] = new InterestCategory(1, "agriculture, environmental and animal care"),
                ["Business and administration"] = new InterestCategory(2, "business and administration"),
                ["Care services"] = new InterestCategory(3, "care services"),
                ["Catering and hospitality"] = new InterestCategory(4, "catering and hospitality"),
                ["Construction and building"] = new InterestCategory(5, "construction and building"),
                ["Creative and design"] = new InterestCategory(6, "creative and design"),
                ["Digital"] = new InterestCategory(7, "digital"),
                ["Education and early years"] = new InterestCategory(8, "education and early years"),
                ["Engineering and manufacturing"] = new InterestCategory(9, "engineering and manufacturing"),
                ["Hair and beauty"] = new InterestCategory(10, "hair and beauty"),
                ["Health and science"] = new InterestCategory(11, "health and science"),
                ["Legal, finance and accounting"] = new InterestCategory(12, "legal, finance and accounting"),
                ["Protective services"] = new InterestCategory(13, "protective services"),
                ["Sales and marketing"] = new InterestCategory(14, "sales and marketing"),
                ["Transport and logistics"] = new InterestCategory(15, "transport and logistics"),
            };

        public static bool TryGet(string pageTitle, out InterestCategory category)
        {
            category = null;
            return !string.IsNullOrWhiteSpace(pageTitle)
                   && Categories.TryGetValue(pageTitle.Trim(), out category);
        }
    }
}
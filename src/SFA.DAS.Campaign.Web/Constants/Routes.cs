using System.Collections.Generic;

namespace SFA.DAS.Campaign.Web.Constants
{
    public static class Routes
    {
        static Dictionary<string, string> _routes = new Dictionary<string, string>
        {
            {"Agriculture-environmental-and-animal-care", "Agriculture, environmental and animal care"},
            {"Business-and-administration", "Business and administration"},
            {"Care-services", "Care services"},
            {"Catering-and-hospitality", "Catering and hospitality"},
            {"Construction", "Construction"},
            {"Creative-and-design", "Creative and design"},
            {"Education-and-childcare", "Education and childcare"},
            {"Engineering-and-manufacturing", "Engineering and manufacturing"},
            {"Hair-and-beauty", "Hair and beauty"},
            {"Health-and-science", "Health and science"},
            {"Legal-finance-and-accounting", "Legal, finance and accounting"},
            {"Protective-services", "Protective services"},
            {"Sales-marketing-and-procurement", "Sales, marketing and procurement"},
            {"Transport-and-logistics", "Transport and logistics"},
            {"Digital", "Digital"},
        };

        public static string GetRoute(string word)
        {
            // Try to get the result in the static Dictionary
            string result;

            if (_routes.TryGetValue(word, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

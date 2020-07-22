using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.Campaign.Content.ContentTypes
{
    public class InterestsPage : ContentBase
    {
        public string Title { get; set; }
        public List<Interest> Interests { get; set; }

        public IEnumerable<IGrouping<int, Interest>> GetGroupedList()
        {
            var result = Interests
                .Select((value, index) => new { Value = value, Index = index })
                .GroupBy(i => i.Index / 4, v => v.Value);

            return result;
        }
    }
}
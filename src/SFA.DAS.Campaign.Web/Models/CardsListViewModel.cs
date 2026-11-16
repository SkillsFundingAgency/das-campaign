using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.Campaign.Web.Models
{
    /// <summary>
    /// A list of linked cardsRender with: &lt;partial name="Components/_CardsList" model="model" /&gt;
    /// </summary>
    public class CardsListViewModel
    {
        public const int DefaultHeadingLevel = 2;
        public const int DefaultSubHeadingLevel = 3;

        /// <summary>Heading level (1-6) used for each card's title. Defaults to 3.</summary>
        public int SubHeadingLevel { get; set; } = DefaultSubHeadingLevel;

        public List<CardsListItem> Items { get; set; }

        /// <summary>Items that can be rendered. The GOV.UK component requires a href for every card, so items without a URL are dropped.</summary>
        public IEnumerable<CardsListItem> RenderableItems => Items?.Where(i => i != null && !string.IsNullOrWhiteSpace(i.Url)) ?? Enumerable.Empty<CardsListItem>();

        public bool HasItems => RenderableItems.Any();
        
        public string SubHeadingTag => $"h{ClampHeadingLevel(SubHeadingLevel, DefaultSubHeadingLevel)}";

        private static int ClampHeadingLevel(int level, int fallback)
        {
            return level >= 1 && level <= 6 ? level : fallback;
        }
    }

    public class CardsListItem
    {
        /// <summary>The link text, shown as the card's heading.</summary>
        public string Text { get; set; }
        
        /// <summary>URL of the link.</summary>
        public string Url { get; set; }
        
        /// <summary>Optional tag shown under the heading.</summary>
        public string Tag { get; set; }

        /// <summary>Optional supporting text shown under the heading.</summary>
        public string Description { get; set; }

        /// <summary>Optional small-print line shown below the description, such as a date</summary>
        public string Meta { get; set; }
    }
}

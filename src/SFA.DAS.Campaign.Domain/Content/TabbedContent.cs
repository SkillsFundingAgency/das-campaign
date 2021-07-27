using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class TabbedContent
    {
        public string TabName { get; set; }
        public string TabTitle { get; set; }
        public bool FindTraineeShip { get; set; }
        public IEnumerable<IHtmlControl> Content { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class OrderedList : IHtmlControl
    {
        public OrderedList()
        {
            Items = new List<List<string>>();
        }

        public List<List<string>> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Models
{
    public class EmailPreferences
    {
        public string EncodedEmail { get; set; }
        public bool ReceiveEmails { get; set; }
    }
}

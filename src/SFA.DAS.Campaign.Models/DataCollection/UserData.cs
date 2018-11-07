using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Models.DataCollection
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Consent { get; set; }
        public bool EmailVerified { get; set; }
        public string CookieId { get; set; }
        public string RouteId { get; set; }
    }
}

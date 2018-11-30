﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel : FormModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public bool AcceptTandCs { get; set; }
        
    }
}

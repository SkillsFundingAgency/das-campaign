using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Validators;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ApprenticeshipTrainingAndBenefitsViewModel
    {
        public List<StandardResponse> Standards { get; set; }
        public IEnumerable<SelectListItem> StandardItems { get { return Standards?.Select(s => new SelectListItem($"{s.Title} (Level {s.Level})", s.StandardUId)); } }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Panel Panel1 { get; set; }
        public Panel Panel2 { get; set; }
        public Panel Panel3 { get; set; }

        [Required(ErrorMessage = "Select a training course")]
        public string StandardUid { get; set; }
        public CalculationQueryResult CalculationResults { get; set; }
        public bool Submitted { get; set; }

        [Required(ErrorMessage = "Select your annual pay bill")]
        public bool? PayBillGreaterThanThreeMillion { get; set; }

        [Required(ErrorMessage = "Select your number of employees")]
        public bool? OverFiftyEmployees { get; set; }

        [Required(ErrorMessage = "Enter how many roles you have available for this apprenticeship")]
        [IsNumeric] 
        public string NumberOfRoles { get; set; }

        public int? Roles
        {
            get { if (int.TryParse(NumberOfRoles, out var n)) return n; return null; }
        }
    }
}

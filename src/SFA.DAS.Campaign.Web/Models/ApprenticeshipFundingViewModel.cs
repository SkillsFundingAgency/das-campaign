using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ApprenticeshipFundingViewModel
    {
        public List<StandardResponse> Standards { get; set; }
        public IEnumerable<SelectListItem> StandardItems { get { return Standards?.Select(s => new SelectListItem($"{s.Title} (Level {s.Level})", s.StandardUId)); } }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Panel Panel1 { get; set; }
        public Panel Panel2 { get; set; }
        public string StandardUid { get; set; }
        public CalculationQueryResult CalculationResults { get; set; }
        public bool Submitted { get; set; }
        public bool? PayBillGreaterThanThreeMillion { get; set; }
        public bool? OverFiftyEmployees { get; set; }
        public int NumberOfRoles { get; set; }
    }


}

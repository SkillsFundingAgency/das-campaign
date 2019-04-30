using System;
using Sfa.Das.Sas.Shared.Components.Domain;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace SFA.DAS.Campaign.Web.Models.Fat
{
    public class TableCssViewModel : ITableCssViewModel
    {
        public string Table => "tableGDS";
        public string Head => "table__headGDS";
        public string Header => "table__headerGDS";
        public string Row => "table__rowGDS";
        public string Body => "table__bodyGD";
        public string Cell => "table__cellGDS";
    }
}

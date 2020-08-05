using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents
{
    public class FatSearchViewModel : SearchQueryViewModel
    {
        public string FatSearchRoute { get; set; } = "/Fat/Search";
    }
}

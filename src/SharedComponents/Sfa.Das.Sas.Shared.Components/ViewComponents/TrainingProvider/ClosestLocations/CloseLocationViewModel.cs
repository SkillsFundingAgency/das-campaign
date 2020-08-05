namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class CloseLocationViewModel
    {
        public int LocationId { get; set; }
        public double Distance { get; set; }
        public string PostCode { get; set; }
        public string AddressWithoutPostCode { get; set; }
    }
}
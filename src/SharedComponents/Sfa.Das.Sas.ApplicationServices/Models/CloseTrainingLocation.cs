namespace Sfa.Das.Sas.ApplicationServices.Models
{
    using Sfa.Das.Sas.Core.Domain.Model;

    public class CloseTrainingLocation
    {
        public double Distance { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public Address Address { get; set; }
    }
}
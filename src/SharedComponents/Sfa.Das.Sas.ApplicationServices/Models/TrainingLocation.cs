namespace Sfa.Das.Sas.ApplicationServices.Models
{
    using Sfa.Das.Sas.Core.Domain.Model;

    public class TrainingLocation
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public Address Address { get; set; }

        public object Location { get; set; }

        public object LocationPoint { get; set; }
    }
}
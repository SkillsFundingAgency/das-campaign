namespace Sfa.Das.Sas.Core.Domain.Model
{
    public class Location
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public Address Address { get; set; }
    }
}
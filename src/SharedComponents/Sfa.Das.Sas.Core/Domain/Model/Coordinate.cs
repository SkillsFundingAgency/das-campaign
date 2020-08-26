namespace Sfa.Das.Sas.Core.Domain.Model
{
    public sealed class Coordinate
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public override string ToString()
        {
            return $"Longitude: {Lon}, Latitude: {Lat}";
        }
    }
}

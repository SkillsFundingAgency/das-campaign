namespace SFA.DAS.Campaign.Application.Geocode
{
    public sealed class Coordinates
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public override string ToString()
        {
            return $"Longitude: {Lon}, Latitude: {Lat}";
        }
    }
}

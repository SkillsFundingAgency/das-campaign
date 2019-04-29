using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Models.Geocode
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

﻿namespace SFA.DAS.Campaign.Application.Geocode
{
    public sealed class CoordinatesResponse
    {
        public Coordinates Coordinates { get; set; }
        public string Country { get; set; }
        public string ResponseCode { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Models.Geocode
{
    public sealed class CoordinatesResponse
    {
        public Coordinates Coordinates { get; set; }

        public string ResponseCode { get; set; }
    }
}
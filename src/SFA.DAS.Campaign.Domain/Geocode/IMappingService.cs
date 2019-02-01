﻿using System.Collections.Generic;
using SFA.DAS.Campaign.Models.Vacancy;

namespace SFA.DAS.Campaign.Application.Geocode
{
    public interface IMappingService
    {
        string GetStaticMapsUrl(Location coordinates);
        string GetStaticMapsUrl(IEnumerable<Location> locations);
        string GetStaticMapsUrl(IEnumerable<Location> locations, string height, string width);
    }
}
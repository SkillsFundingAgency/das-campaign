using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ApprenticeshipFavouriteRead
    {
        public ApprenticeshipFavouriteRead()
        {
            Providers = new List<ApprenticeshipProviderFavourite>();
        }

        public ApprenticeshipFavouriteRead(string apprenticeshipId) : this()
        {
            ApprenticeshipId = apprenticeshipId;
        }
        public string ApprenticeshipId { get; set; }
        public IList<ApprenticeshipProviderFavourite> Providers { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int Level { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public ApprenticeshipType ApprenticeshipType { get; set; }
        public bool Active { get; set; }
    }
}
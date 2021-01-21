﻿using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.SearchResults;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFatSearchResultsViewModelMapper
    {
        FatSearchResultsViewModel Map(ApprenticeshipSearchResults item);

    }
}

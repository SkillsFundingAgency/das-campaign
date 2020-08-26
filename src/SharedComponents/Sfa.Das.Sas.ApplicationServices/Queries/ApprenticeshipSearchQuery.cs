using System.Collections.Generic;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices.Queries
{
    public sealed class ApprenticeshipSearchQuery : IRequest<ApprenticeshipSearchResponse>
    {
        public string Keywords { get; set; }

        public int Page { get; set; }

        public int Order { get; set; }

        public List<int> SelectedLevels { get; set; }
    }
}
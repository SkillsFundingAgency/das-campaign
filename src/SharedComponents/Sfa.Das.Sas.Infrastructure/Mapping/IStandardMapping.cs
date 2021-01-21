using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;
using ApiStandard = SFA.DAS.Apprenticeships.Api.Types.Standard;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    public interface IStandardMapping
    {
        Standard MapToStandard(ApiStandard document);
        Standard MapToStandard(StandardSearchResultsItem document);
    }
}
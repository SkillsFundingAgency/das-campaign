using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;
using ApiFramework = SFA.DAS.Apprenticeships.Api.Types.Framework;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    public interface IFrameworkMapping
    {
        Framework MapToFramework(FrameworkSearchResultsItem document);
        Framework MapToFramework(ApiFramework document);
    }
}
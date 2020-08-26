using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Core.Domain.Services
{
    public interface IGetAssessmentOrganisations
    {
        Task<IEnumerable<AssessmentOrganisation>> GetByStandardId(int id);

    }
}

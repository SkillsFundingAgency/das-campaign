using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices.Services
{
    public interface IPaginationOrientationService
    {
        PaginationOrientationDetails GeneratePaginationDetails(int page, int take, long totalHits);
    }
}

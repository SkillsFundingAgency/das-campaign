using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices.Services
{
    public class PaginationOrientationService : IPaginationOrientationService
    {
        public PaginationOrientationDetails GeneratePaginationDetails(int page, int take, long totalHits)
        {
            var pageNumber = page <= 0 ? 1 : page;

            var res = default(PaginationOrientationDetails);

            var skip = (pageNumber - 1) * take;

            while (skip >= totalHits)
            {
                pageNumber = pageNumber - 1;
                skip = skip - take;
            }

            var lastPage = take > 0 ? (int)System.Math.Ceiling((double)totalHits / take) : 1;

            res.LastPage = lastPage;
            res.CurrentPage = pageNumber;
            res.Skip = skip;

            return res;
        }
    }
}
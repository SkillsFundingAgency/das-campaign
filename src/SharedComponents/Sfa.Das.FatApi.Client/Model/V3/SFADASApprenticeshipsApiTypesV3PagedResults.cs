using System.Collections.Generic;

namespace Sfa.Das.FatApi.Client.Model
{
    public abstract class SFADASApprenticeshipsApiTypesV3PagedResults<T> where T : class
    {
        public long TotalResults { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IList<T> Results { get; set; }
    }
}

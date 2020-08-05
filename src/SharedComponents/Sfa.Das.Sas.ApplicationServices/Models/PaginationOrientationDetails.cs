namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public struct PaginationOrientationDetails
    {
        public int Skip { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
    }
}
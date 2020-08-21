namespace Sfa.Das.Sas.Core
{
    public static class StringExtensions
    {
        public static string AddSlash(this string self) => self.EndsWith("/") ? self : self + "/";
    }
}

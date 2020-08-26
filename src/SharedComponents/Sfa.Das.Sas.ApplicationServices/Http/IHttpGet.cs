namespace Sfa.Das.Sas.ApplicationServices.Http
{
    public interface IHttpGet
    {
        string Get(string url, string username, string pwd);
    }
}

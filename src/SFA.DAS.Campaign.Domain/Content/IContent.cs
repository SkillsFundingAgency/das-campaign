namespace SFA.DAS.Campaign.Domain.Content
{
    public interface IContent<T>
    {
        T Content { get; set; }
        string Render<T>();
    }
}

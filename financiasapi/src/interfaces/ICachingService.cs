namespace financias.src.interfaces
{
    public interface ICachingService
    {
        Task<string> Get(string key);
        Task Save(string key, string value);
    }
}
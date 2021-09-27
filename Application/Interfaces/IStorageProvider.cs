namespace Weboku.Application.Interfaces
{
    public interface IStorageProvider
    {
        void Save<T>(string key, T value);
        T Load<T>(string key);
        bool HasKey(string key);
    }
}
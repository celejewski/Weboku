using Application.Interfaces;
using Blazored.LocalStorage;

namespace UI.BlazorWASM.Providers
{
    public class StorageProvider : IStorageProvider
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public StorageProvider(ISyncLocalStorageService syncLocalStorageService)
        {
            _syncLocalStorageService = syncLocalStorageService;
        }

        public T Load<T>(string key)
        {
            return _syncLocalStorageService.GetItem<T>(key);
        }

        public void Save<T>(string key, T data)
        {
            _syncLocalStorageService.SetItem(key, data);
        }

        public bool HasKey(string key)
        {
            return _syncLocalStorageService.ContainKey(key);
        }
    }
}

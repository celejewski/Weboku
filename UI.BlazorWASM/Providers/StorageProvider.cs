using Blazored.LocalStorage;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class StorageProvider
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public StorageProvider(ISyncLocalStorageService syncLocalStorageService)
        {
            _syncLocalStorageService = syncLocalStorageService;
        }

        public T Load<T>(StorageKey storageKey)
        {
            return _syncLocalStorageService.GetItem<T>(storageKey.ToString());
        }

        public void Save<T>(StorageKey storageKey, T data)
        {
            _syncLocalStorageService.SetItem(storageKey.ToString(), data);
        }

        public bool HasSaved(StorageKey storageKey)
        {
            return _syncLocalStorageService.ContainKey(storageKey.ToString());
        }
    }
}

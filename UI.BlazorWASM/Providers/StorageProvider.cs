using Blazored.LocalStorage;
using Core.Converters;
using Core.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class StorageProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IGridConverter _converter = new Base64CandidatesConverter();

        public StorageProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public Task<bool> HasSavedGrid()
        {
            return _localStorageService.ContainKeyAsync(StorageKey.Grid.ToString());
        }

        public Task SaveGrid(IGrid grid)
        {
            var converted = _converter.Serialize(grid);
            return _localStorageService.SetItemAsync<string>(StorageKey.Grid.ToString(), converted);
        }
        public async Task<IGrid> LoadGrid()
        {
            var text = await _localStorageService.GetItemAsync<string>(StorageKey.Grid.ToString());
            if (!_converter.IsValidFormat(text))
            {
                System.Console.WriteLine("Invalid grid");
                return new Grid();
            }
            return _converter.Deserialize(text);
        }

        public Task<bool> HasSavedSudoku()
        {
            return _localStorageService.ContainKeyAsync(StorageKey.Sudoku.ToString());
        }

        public Task SaveSudoku(Sudoku sudoku)
        {
            return _localStorageService.SetItemAsync<Sudoku>(StorageKey.Sudoku.ToString(), sudoku);
        }
        public async Task<Sudoku> LoadSudoku()
        {
            return await _localStorageService.GetItemAsync<Sudoku>(StorageKey.Sudoku.ToString());
        }

        public Task Save<T>(StorageKey key, T value)
        {
            return _localStorageService.SetItemAsync<T>(key.ToString(), value);
        }

        public Task<bool> HasSaved(StorageKey key)
        {
            return _localStorageService.ContainKeyAsync(key.ToString());
        }

        public Task SavePreferredDifficulty(string preferredDifficulty) => Save(StorageKey.PreferredDifficulty, preferredDifficulty);
        public Task<string> LoadPreferredDifficulty()
        {
            return _localStorageService.GetItemAsync<string>(StorageKey.PreferredDifficulty.ToString());
        }
    }
}

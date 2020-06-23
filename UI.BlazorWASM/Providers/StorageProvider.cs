using Blazored.LocalStorage;
using Core.Converters;
using Core.Data;
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
            var converted = _converter.ToText(grid);
            return _localStorageService.SetItemAsync<string>(StorageKey.Grid.ToString(), converted);
        }
        public async Task<IGrid> LoadGrid()
        {
            var text = await _localStorageService.GetItemAsync<string>(StorageKey.Grid.ToString());
            return _converter.FromText(text);
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
    }
}

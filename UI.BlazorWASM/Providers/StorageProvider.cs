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
            if (!_converter.IsValidText(text))
            {
                System.Console.WriteLine("Invalid grid");
                return new Grid();
            }
            return _converter.FromText(text);
        }

        public Task<bool> HasSavedSudoku()
        {
            return _localStorageService.ContainKeyAsync(StorageKey.Sudoku.ToString());
        }

        public Task SaveSudoku(SudokuV1 sudoku)
        {
            return _localStorageService.SetItemAsync<SudokuV1>(StorageKey.Sudoku.ToString(), sudoku);
        }
        public async Task<SudokuV1> LoadSudoku()
        {
            return await _localStorageService.GetItemAsync<SudokuV1>(StorageKey.Sudoku.ToString());
        }
    }
}

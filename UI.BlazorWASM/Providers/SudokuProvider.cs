using Core.Data;
using Core.Serializer;
using Core.Solvers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Providers
{
    public class SudokuProvider : IProvider
    {
        private Sudoku _sudoku = new Sudoku();
        private readonly ISolver _solver = new BruteForceSolver();
        private readonly IGridSerializer _converter;

        private readonly HttpClient _httpClient;

        public async Task<Sudoku> Generate(string difficulty)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Sudoku>($"http://andzej-002-site2.ftempurl.com/sudokugenerator/{difficulty}");
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex.ToString());
                return new Sudoku();
            }
        }
        public SudokuProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _converter = GridSerializerFactory.Make(GridSerializerName.Default);
        }

        public Sudoku Sudoku
        {
            get => _sudoku;
            set
            {
                _sudoku = value;
                if( _converter.IsValidFormat(_sudoku.Given) )
                {
                    var grid = _converter.Deserialize(_sudoku.Given);
                    _solution = _solver.Solve(grid);
                }
                OnChanged?.Invoke();
            }
        }

        public bool IsUserCreatingCustomSudoku { get; set; }

        public string Difficulty => Sudoku.Difficulty;
        public string PreferredDifficulty
        {
            get => _preferredDifficulty;
            set
            {
                _preferredDifficulty = value;
                OnChanged?.Invoke();
            }
        }

        private IGrid _solution;
        private string _preferredDifficulty;

        public InputValue GetSolution(Position pos) => _solution?.GetValue(pos) ?? InputValue.None;
        public bool HasSolution => !IsUserCreatingCustomSudoku && _solution != null;

        public event Action OnChanged;
    }
}

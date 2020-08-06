using Core.Converters;
using Core.Data;
using Core.Solvers;
using System;

namespace UI.BlazorWASM.Providers
{
    public class SudokuProvider : IProvider
    {
        private Sudoku _sudoku = new Sudoku();
        private readonly ISolver _solver = new BruteForceSolver();
        private readonly IGridConverter _converter;

        public SudokuProvider(ChainGridConverter chainGridConverter)
        {
            _converter = chainGridConverter;
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

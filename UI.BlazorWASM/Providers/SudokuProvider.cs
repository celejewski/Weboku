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
                if (_converter.IsValidText(_sudoku.Given))
                {
                    var grid = _converter.FromText(_sudoku.Given);
                    _solution = _solver.Solve(grid);
                }
                OnChanged?.Invoke();
            }
        }

        public string Difficulty => Sudoku.Difficulty;

        private IGrid _solution;
        public InputValue GetSolution(Position pos) =>  _solution?.GetValue(pos) ?? InputValue.Empty;
        public bool HasSolution => _solution != null;

        public event Action OnChanged;
    }
}

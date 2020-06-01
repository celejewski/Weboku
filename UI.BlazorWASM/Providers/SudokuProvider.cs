using Core.Data;
using Core.Generators;
using System;

namespace UI.BlazorWASM.Providers
{
    public class SudokuProvider : ISudokuProvider
    {
        public SudokuProvider(IEmptyGridGenerator generator)
        {
            _grid = generator.Empty();
        }

        private readonly IGrid _grid;

        public ICell[,] Cells => _grid.Cells;

        public event Action OnCandidatesChanged;
        public event Action OnValueChanged;
        public event Action OnValueOrCandidatesChanged;

        public void SetValue(int x, int y, int value)
        {
            _grid.SetValue(x, y, value);
            OnValueChanged?.Invoke();
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }
        public void ToggleCandidate(int x, int y, int value)
        {
            _grid.ToggleCandidate(x, y, value);
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }

        public void AssignFrom(IGrid source)
        {
            _grid.AssignFrom(source);
            OnValueChanged?.Invoke();
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }

        public void FillAllCandidates()
        {
            _grid.FillAllCandidates();
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }
        
        public void ClearAllCandidates()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    _grid.Cells[x, y].Candidates.Clear();
                }
            }
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }

        public IGrid GetGridClone() => (IGrid) _grid.Clone();

        public void Restart()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    if (!_grid.Cells[x, y].IsGiven)
                    {
                        _grid.SetValue(x, y, 0);
                    }
                    _grid.Cells[x, y].Candidates.Clear();
                }
            }
            OnValueChanged?.Invoke();
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }

        public Sudoku Sudoku { get; set; } = new Sudoku();
    }
}

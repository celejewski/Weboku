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

        public event Action OnChanged;

        public void SetValue(int x, int y, int value)
        {
            _grid.SetValue(x, y, value);
            OnChanged?.Invoke();
        }
        public void ToggleCandidate(int x, int y, int value)
        {
            _grid.ToggleCandidate(x, y, value);
            OnChanged?.Invoke();
        }

        public void AssignFrom(IGrid source)
        {
            _grid.AssignFrom(source);
            OnChanged?.Invoke();
        }

        public void FillAllCandidates()
        {
            _grid.FillAllCandidates();
            OnChanged?.Invoke();
        }
        public IGrid GetGridClone() => (IGrid) _grid.Clone();
    }
}

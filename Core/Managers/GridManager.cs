using Core.Data;
using System;

namespace Core.Managers
{
    internal sealed class GridManager
    {
        public GridManager()
        {
            Grid = new Grid();
        }

        private IGrid _grid;
        public IGrid Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                ValueAndCandidateChanged();
            }
        }

        public bool IsValueLegal(Position pos)
        {
            return Grid.IsCandidateLegal(pos, Grid.GetValue(pos));
        }
        public InputValue GetInputValue(Position pos)
        {
            return Grid.GetValue(pos);
        }

        public bool HasValue(Position pos)
        {
            return Grid.HasValue(pos);
        }

        public bool HasCandidate(Position pos, InputValue value)
        {
            return Grid.HasCandidate(pos, value);
        }

        public bool IsGiven(Position pos)
        {
            return Grid.GetIsGiven(pos);
        }

        public int GetCandidatesCount(Position pos)
        {
            return _grid.GetCandidates(pos).Count();
        }

        public void ValueChanged()
        {
            OnValueChanged?.Invoke();
        }

        public void CandidateChanged()
        {
            OnCandidateChanged?.Invoke();
            OnValueOrCandidateChanged?.Invoke();
        }

        public void FillAllLegalCandidates()
        {
            Grid.FillAllLegalCandidates();
        }

        public bool IsCandidateLegal(Position pos, InputValue value)
        {
            return Grid.IsCandidateLegal(pos, value);
        }

        public void ValueAndCandidateChanged()
        {
            OnValueChanged?.Invoke();
            OnCandidateChanged?.Invoke();
            OnValueOrCandidateChanged?.Invoke();
        }

        public event Action OnValueChanged;
        public event Action OnCandidateChanged;
        public event Action OnValueOrCandidateChanged;
    }
}

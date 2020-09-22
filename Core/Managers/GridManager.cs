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

        public bool IsValueLegal(Position position)
        {
            return Grid.IsCandidateLegal(position, Grid.GetValue(position));
        }
        public Value GetInputValue(Position position)
        {
            return Grid.GetValue(position);
        }

        public bool HasValue(Position position)
        {
            return Grid.HasValue(position);
        }

        public bool HasCandidate(Position position, Value value)
        {
            return Grid.HasCandidate(position, value);
        }

        public bool IsGiven(Position pos)
        {
            return Grid.GetIsGiven(pos);
        }

        public int GetCandidatesCount(Position position)
        {
            return _grid.GetCandidates(position).Count();
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

        public bool IsCandidateLegal(Position position, Value value)
        {
            return Grid.IsCandidateLegal(position, value);
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

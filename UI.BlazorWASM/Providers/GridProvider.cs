using Core.Data;
using System;
using System.Linq;

namespace UI.BlazorWASM.Providers
{
    public class GridProvider : IGridProvider
    {
        private IGrid _grid = new Grid();
        private readonly bool[,] _isInputLegal = new bool[9, 9];
        private readonly bool[,,] _isCandidateLegal = new bool[9, 9, 10];
        private readonly SudokuProvider _sudokuProvider;

        public IGrid Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                CalcIsInputLegal();
                CalcIsCandidateLegal();
                ValueAndCandidatesChanged();
            }
        }

        public event Action OnCandidatesChanged;
        public event Action OnValueChanged;
        public event Action OnValueOrCandidatesChanged;

        public GridProvider(SudokuProvider sudokuProvider)
        {
            ResetIsInputLegal();
            ResetIsCandidateLegal();
            _sudokuProvider = sudokuProvider;
        }

        public void AddCandidate(Position pos, InputValue value)
        {
            _grid.AddCandidate(pos, value);
            _isCandidateLegal[pos.x, pos.y, value] = GridHelper.IsLegal(pos, value, Grid);
            CandidatesChanged();
        }

        public void ClearCandidates()
        {
            _grid.ClearCandidates();
            CandidatesChanged();
        }

        public void ClearCandidates(Position pos)
        {
            _grid.ClearCandidates(pos);
            CandidatesChanged();
        }

        public void FillCandidates()
        {
            _grid.FillCandidates();
            CandidatesChanged();
        }

        public int CandidatesCount(Position pos)
        {
            return _grid.CandidatesCount(pos);
        }

        public InputValue GetValue(Position pos)
        {
            return _grid.GetValue(pos);
        }

        public bool HasCandidate(Position pos, InputValue value)
        {
            return _grid.HasCandidate(pos, value);
        }

        public bool IsCandidateLegal(Position pos, InputValue value)
        {
            return _isCandidateLegal[pos.x, pos.y, value];
        }

        public bool GetIsGiven(Position pos)
        {
            return _grid.GetIsGiven(pos);
        }

        public bool IsValueLegal(Position pos)
        {
            if( _sudokuProvider.HasSolution )
            {
                return GetIsGiven(pos)
                    || GetValue(pos) == InputValue.Empty
                    || GetValue(pos) == _sudokuProvider.GetSolution(pos.x, pos.y);
            }

            return GetValue(pos) == InputValue.Empty || _isInputLegal[pos.x, pos.y];
        }

        public void RemoveCandidate(Position pos, InputValue value)
        {
            _grid.RemoveCandidate(pos, value);
            CandidatesChanged();
        }

        public void SetValue(Position pos, InputValue value)
        {
            if( value != InputValue.Empty )
            {
                _grid.SetValue(pos, value);
                ClearCandidates(pos);
                foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos) )
                {
                    _grid.RemoveCandidate(coords, value);
                }
                _isInputLegal[pos.x, pos.y] = GridHelper.IsLegal(pos, value, Grid);
                ValueAndCandidatesChanged();
            }
            else
            {
                _grid.SetValue(pos, value);
                ValueChanged();
            }
        }

        public void ToggleCandidate(Position pos, InputValue value)
        {
            _isCandidateLegal[pos.x, pos.y, value] = GridHelper.IsLegal(pos, value, Grid);
            _grid.ToggleCandidate(pos, value);
            CandidatesChanged();
        }

        public void FillAllLegalCandidates()
        {
            _grid.FillCandidates();
            foreach( var pos in Position.All.Where(pos => _grid.HasValue(pos)) )
            {
                foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos) )
                {
                    _grid.RemoveCandidate(coords, _grid.GetValue(pos));
                }
            }
            ResetIsCandidateLegal();
            CandidatesChanged();
        }

        private void ResetIsCandidateLegal()
        {
            foreach( var pos in Position.All )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    _isCandidateLegal[pos.x, pos.y, value] = true;
                }
            }
        }

        private void ResetIsInputLegal()
        {
            foreach( var pos in Position.All )
            {
                _isInputLegal[pos.x, pos.y] = true;
            }
        }

        private void CalcIsInputLegal()
        {
            foreach( var pos in Position.All )
            {
                _isInputLegal[pos.x, pos.y] = GridHelper.IsLegal(pos, GetValue(pos), Grid);
            }
        }

        private void CalcIsCandidateLegal()
        {
            foreach( var pos in Position.All.Where(pos => !HasValue(pos)) )
            {
                foreach( var value in InputValue.NonEmpty.Where(value => HasCandidate(pos, value)) )
                {
                    _isCandidateLegal[pos.x, pos.y, value] = GridHelper.IsLegal(pos, value, Grid);
                }
            }
        }

        private void ValueChanged()
        {
            OnValueOrCandidatesChanged?.Invoke();
            OnValueChanged?.Invoke();

        }
        private void CandidatesChanged()
        {
            OnValueOrCandidatesChanged?.Invoke();
            OnCandidatesChanged?.Invoke();
        }

        private void ValueAndCandidatesChanged()
        {
            OnValueChanged?.Invoke();
            OnCandidatesChanged?.Invoke();
            OnValueOrCandidatesChanged?.Invoke();
        }

        public bool HasValue(Position pos) => _grid.HasValue(pos);
    }
}

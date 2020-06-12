using Core.Data;
using System;
using System.Threading;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Providers
{
    public class GridProvider : IGridProvider
    {
        private IGridV2 _grid = new GridV2();
        private readonly bool[,] _isInputLegal = new bool[9, 9];
        private readonly bool[,,] _isCandidateLegal = new bool[9, 9, 10];

        public IGridV2 Grid
        {
            get => _grid;
            set 
            {
                _grid = value;
                ValueAndCandidatesChanged();
            }
        }

        public event Action OnCandidatesChanged;
        public event Action OnValueChanged;
        public event Action OnValueOrCandidatesChanged;

        public GridProvider()
        {
            ResetIsInputLegal();
            ResetIsCandidateLegal();
        }

        public void AddCandidate(int x, int y, InputValue value)
        {
            _grid.AddCandidate(x, y, value);
            _isCandidateLegal[x, y, (int) value] = GridHelper.IsLegal(x, y, value, this);
            CandidatesChanged();
        }

        public void ClearCandidates()
        {
            _grid.ClearCandidates();
            CandidatesChanged();
        }

        public void ClearCandidates(int x, int y)
        {
            _grid.ClearCandidates(x, y);
            CandidatesChanged();
        }

        public void FillCandidates()
        {
            _grid.FillCandidates();
            CandidatesChanged();
        }

        public int GetCandidatesCount(int x, int y)
        {
            var count = 0;
            for( int value = 1; value < 10; value++ )
            {
                if( _grid.HasCandidate(x, y, (InputValue) value) )
                {
                    count += 1;
                }
            }
            return count;
        }

        public InputValue GetValue(int x, int y)
        {
            return _grid.GetValue(x, y);
        }

        public bool HasCandidate(int x, int y, InputValue value)
        {
            return _grid.HasCandidate(x, y, value);
        }

        public bool IsCandidateLegal(int x, int y, InputValue value)
        {
            return _isCandidateLegal[x, y, (int) value];
        }

        public bool GetIsGiven(int x, int y)
        {
            return _grid.GetIsGiven(x, y);
        }

        public bool IsValueLegal(int x, int y)
        {
            return GetValue(x, y) == InputValue.Empty || _isInputLegal[x, y];
        }

        public void RemoveCandidate(int x, int y, InputValue value)
        {
            _grid.RemoveCandidate(x, y, value);
            CandidatesChanged();
        }

        public void SetValue(int x, int y, InputValue value)
        {
            if (value != InputValue.Empty)
            {
                _grid.SetValue(x, y, value);
                ClearCandidates(x, y);
                foreach( var coords in GridHelper.GetCoordsWhichCanSee(x, y))
                {
                    _grid.RemoveCandidate(coords.X, coords.Y, value);
                }
                _isInputLegal[x, y] = GridHelper.IsLegal(x, y, value, this);
                ValueAndCandidatesChanged();
            }
            else
            { 
                _grid.SetValue(x, y, value);
                ValueChanged();
            }
        }

        public void ToggleCandidate(int x, int y, InputValue value)
        {
            _isCandidateLegal[x, y, (int) value] = GridHelper.IsLegal(x, y, value, this);
            _grid.ToggleCandidate(x, y, value);
            CandidatesChanged();
        }

        public void FindAllCandidates()
        {
            _grid.FillCandidates();
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    if (_grid.GetValue(x, y) == InputValue.Empty)
                    {
                        continue;
                    }

                    foreach( var coords in GridHelper.GetCoordsWhichCanSee(x, y) )
                    {
                        _grid.RemoveCandidate(coords.X, coords.Y, _grid.GetValue(x, y));    
                    }
                }
            }

            ResetIsCandidateLegal();
            CandidatesChanged();
        }

        private void ResetIsCandidateLegal()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int value = 0; value < 10; value++ )
                    {
                        _isCandidateLegal[x, y, value] = true;
                    }
                }
            }
        }

        private void ResetIsInputLegal()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    _isInputLegal[x, y] = true;
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
    }
}

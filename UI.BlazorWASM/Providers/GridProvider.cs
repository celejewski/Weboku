using Core.Data;
using System;
using System.Security.Cryptography.X509Certificates;

namespace UI.BlazorWASM.Providers
{
    public class GridProvider : IGridProvider
    {
        private readonly IGridV2 _grid;

        public event Action OnCandidatesChanged;
        public event Action OnValueChanged;
        public event Action OnValueOrCandidatesChanged;

        public void AddCandidate(int x, int y, InputValue value)
        {
            _grid.AddCandidate(x, y, value);
        }

        public void ClearCandidates()
        {
            _grid.ClearCandidates();
        }

        public void ClearCandidates(int x, int y)
        {
            _grid.ClearCandidates(x, y);
        }

        public void FillCandidates()
        {
            _grid.FillCandidates();
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
#warning Not implemented
            return true;
        }

        public bool IsGiven(int x, int y)
        {
#warning Not implemneted
            return false;
        }

        public bool IsValueLegal(int x, int y)
        {
#warning Not implemented
            return true;
        }

        public void RemoveCandidate(int x, int y, InputValue value)
        {
            _grid.RemoveCandidate(x, y, value);
        }

        public void SetValue(int x, int y, InputValue value)
        {
            _grid.SetValue(x, y, value);
        }

        public void ToggleCandidate(int x, int y, InputValue value)
        {
            _grid.ToggleCandidate(x, y, value);
        }
    }
}

using Core.Data;
using System;
using System.Threading;

namespace UI.BlazorWASM.Providers
{
    public class GridProvider : IGridProvider
    {
        public IGridV2 Grid { get; set; } = new GridV2();

        public event Action OnCandidatesChanged;
        public event Action OnValueChanged;
        public event Action OnValueOrCandidatesChanged;

        public void AddCandidate(int x, int y, InputValue value)
        {
            Grid.AddCandidate(x, y, value);
            CandidatesChanged();
        }

        public void ClearCandidates()
        {
            Grid.ClearCandidates();
            CandidatesChanged();
        }

        public void ClearCandidates(int x, int y)
        {
            Grid.ClearCandidates(x, y);
            CandidatesChanged();
        }

        public void FillCandidates()
        {
            Grid.FillCandidates();
            CandidatesChanged();
        }

        public int GetCandidatesCount(int x, int y)
        {
            var count = 0;
            for( int value = 1; value < 10; value++ )
            {
                if( Grid.HasCandidate(x, y, (InputValue) value) )
                {
                    count += 1;
                }
            }
            return count;
        }

        public InputValue GetValue(int x, int y)
        {
            return Grid.GetValue(x, y);
        }

        public bool HasCandidate(int x, int y, InputValue value)
        {
            return Grid.HasCandidate(x, y, value);
        }

        public bool IsCandidateLegal(int x, int y, InputValue value)
        {
#warning Not implemented
            return true;
        }

        public bool GetIsGiven(int x, int y)
        {
            return Grid.GetIsGiven(x, y);
        }

        public bool IsValueLegal(int x, int y)
        {
#warning Not implemented
            return true;
        }

        public void RemoveCandidate(int x, int y, InputValue value)
        {
            Grid.RemoveCandidate(x, y, value);
            CandidatesChanged();
        }

        public void SetValue(int x, int y, InputValue value)
        {
            if (value != InputValue.Empty)
            {
                ClearCandidates(x, y);
            }
            Grid.SetValue(x, y, value);
            ValueChanged();
        }

        public void ToggleCandidate(int x, int y, InputValue value)
        {
            Grid.ToggleCandidate(x, y, value);
            CandidatesChanged();
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
    }
}

using Core.Helpers;

namespace Core.Data
{
    public class Grid : IGrid
    {
        private readonly InputValue[,] _inputs;
        private readonly CandidateValue[,] _candidates;
        private readonly bool[,] _isGivens;
        public Grid()
        {
            _inputs = new InputValue[9, 9];
            _candidates = new CandidateValue[9, 9];
            _isGivens = new bool[9, 9];
        }

        private Grid(InputValue[,] inputs, CandidateValue[,] candidates, bool[,] isGivens)
        {
            _inputs = inputs;
            _candidates = candidates;
            _isGivens = isGivens;
        }

        public InputValue GetValue(int x, int y) => _inputs[x, y];
        public void SetValue(int x, int y, InputValue value) => _inputs[x, y] = value;

        public bool HasCandidate(int x, int y, InputValue value) => (_candidates[x, y] & value.ToCandidateValue()) == value.ToCandidateValue();
        public void ToggleCandidate(int x, int y, InputValue value) => _candidates[x, y] ^= value.ToCandidateValue();
        public void RemoveCandidate(int x, int y, InputValue value) => _candidates[x, y] &= ~value.ToCandidateValue();
        public void AddCandidate(int x, int y, InputValue value) => _candidates[x, y] |= value.ToCandidateValue();

        public void ClearCandidates()
        {
            for( int i = 0; i < 9; i++ )
            {
                for( int j = 0; j < 9; j++ )
                {
                    ClearCandidates(i, j);
                }
            }
        }

        public void ClearCandidates(int x, int y)
        {
            _candidates[x, y] = CandidateValue.None;
        }

        public void FillCandidates()
        {
            for( int i = 0; i < 9; i++ )
            {
                for( int j = 0; j < 9; j++ )
                {
                    if( _inputs[i, j] == InputValue.Empty )
                    {
                        _candidates[i, j] = CandidateValue.All;
                    }
                }
            }
        }

        public bool GetIsGiven(int x, int y)
        {
            return _isGivens[x, y];
        }

        public void SetIsGiven(int x, int y, bool value)
        {
            _isGivens[x, y] = value;
        }

        public IGrid Clone()
        {
            return new Grid(
                (InputValue[,]) _inputs.Clone(),
                (CandidateValue[,]) _candidates.Clone(),
                (bool[,]) _isGivens.Clone()
                );
        }
    }
}

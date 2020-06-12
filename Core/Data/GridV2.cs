namespace Core.Data
{
    class GridV2 : IGridV2
    {
        private readonly InputValue[,] _inputs = new InputValue[9, 9];
        private readonly CandidateValue[,] _candidates = new CandidateValue[9, 9];
        
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
                    _candidates[i, j] = CandidateValue.All;
                }
            }
        }
    }
}

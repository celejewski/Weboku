using System.Linq;
using System.Runtime.CompilerServices;

namespace Core.Data
{
    sealed public class Grid : IGrid
    {
        private readonly InputValue[,] _inputs;
        private readonly Candidates[,] _candidates;
        private readonly bool[,] _isGivens;

        public Grid()
        {
            _inputs = new InputValue[9, 9];
            _candidates = new Candidates[9, 9];
            _isGivens = new bool[9, 9];
        }

        private Grid(InputValue[,] inputs, Candidates[,] candidates, bool[,] isGivens)
        {
            _inputs = inputs;
            _candidates = candidates;
            _isGivens = isGivens;
        }

        public InputValue GetValue(Position position) => _inputs[position.x, position.y];
        public void SetValue(Position position, InputValue value)
        {
            _inputs[position.x, position.y] = value;
            _candidates[position.x, position.y] = Candidates.None;

            if( value != InputValue.None )
            {
                foreach( var seenBy in Position.GetCoordsWhichCanSee(position) )
                {
                    RemoveCandidate(seenBy, value);
                }
            }
        }

        public bool IsCandidateLegal(Position position, InputValue value)
        {
            return value == InputValue.None
                || Position.GetCoordsWhichCanSee(position)
                .Where(otherPosition => !position.Equals(otherPosition))
                .All(otherPosition => GetValue(otherPosition) != value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasCandidate(Position position, InputValue value)
            => (_candidates[position.x, position.y] & value.AsCandidates()) == value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToggleCandidate(Position position, InputValue value)
            => _candidates[position.x, position.y] ^= value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveCandidate(Position position, InputValue value)
            => _candidates[position.x, position.y] &= ~value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCandidate(Position position, InputValue value)
            => _candidates[position.x, position.y] |= value.AsCandidates();

        public void ClearAllCandidates()
        {
            foreach( var position in Position.All )
            {
                ClearCandidates(position);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearCandidates(Position position)
        {
            _candidates[position.x, position.y] = Candidates.None;
        }

        public void FillAllLegalCandidates()
        {
            var cols = new Candidates[9];
            var rows = new Candidates[9];
            var blocks = new Candidates[9];
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var block = (x / 3) + (y / 3) * 3;
                    var candidates = _inputs[x, y].AsCandidates();
                    cols[x] |= candidates;
                    rows[y] |= candidates;
                    blocks[block] |= candidates;
                }
            }

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var block = (x / 3) + (y / 3) * 3;
                    _candidates[x, y] = _inputs[x, y] != InputValue.None
                        ? Candidates.None
                        : Candidates.All ^ (cols[x] | rows[y] | blocks[block]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetIsGiven(Position position)
        {
            return _isGivens[position.x, position.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIsGiven(Position position, bool value)
        {
            _isGivens[position.x, position.y] = value;
        }

        public IGrid Clone()
        {
            return new Grid(
                (InputValue[,]) _inputs.Clone(),
                (Candidates[,]) _candidates.Clone(),
                (bool[,]) _isGivens.Clone()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasValue(Position pos) => GetValue(pos) != InputValue.None;

        public Candidates GetCandidates(Position position) => _candidates[position.x, position.y];
    }
}

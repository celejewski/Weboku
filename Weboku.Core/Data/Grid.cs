using System.Linq;

namespace Weboku.Core.Data
{
    public sealed class Grid
    {
        private readonly Value[,] _values;
        private readonly Candidates[,] _candidates;
        private readonly bool[,] _isGivens;

        public Grid()
        {
            _values = new Value[9, 9];
            _candidates = new Candidates[9, 9];
            _isGivens = new bool[9, 9];
        }

        private Grid(Value[,] inputs, Candidates[,] candidates, bool[,] isGivens)
        {
            _values = inputs;
            _candidates = candidates;
            _isGivens = isGivens;
        }

        public Value GetValue(Position position) => _values[position.x, position.y];

        public void SetValue(Position position, Value value)
        {
            _values[position.x, position.y] = value;
            _candidates[position.x, position.y] = Candidates.None;

            if (value == Value.None) return;

            foreach (var seenBy in Position.GetCoordsWhichCanSeePosition(position))
            {
                RemoveCandidate(seenBy, value);
            }
        }

        public bool IsCandidateLegal(Position position, Value value)
        {
            if (value == Value.None) return true;

            return Position.GetCoordsWhichCanSeePosition(position)
                .Where(otherPosition => !position.Equals(otherPosition))
                .All(otherPosition => GetValue(otherPosition) != value);
        }

        public bool HasCandidate(Position position, Value value)
            => (_candidates[position.x, position.y] & value.AsCandidates()) == value.AsCandidates();

        public void ToggleCandidate(Position position, Value value)
            => _candidates[position.x, position.y] ^= value.AsCandidates();

        public void RemoveCandidate(Position position, Value value)
            => _candidates[position.x, position.y] &= ~value.AsCandidates();

        public void AddCandidate(Position position, Value value)
            => _candidates[position.x, position.y] |= value.AsCandidates();

        public void ClearAllCandidates()
        {
            foreach (var position in Position.Positions)
            {
                ClearCandidates(position);
            }
        }

        public void ClearCandidates(Position position)
        {
            _candidates[position.x, position.y] = Candidates.None;
        }

        public int GetCandidatesCount(Position position)
        {
            var candidates = GetCandidates(position);
            return candidates.Count();
        }

        public void FillAllLegalCandidates()
        {
            var cols = new Candidates[9];
            var rows = new Candidates[9];
            var blocks = new Candidates[9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var block = (x / 3) + (y / 3) * 3;
                    var candidates = _values[x, y].AsCandidates();
                    cols[x] |= candidates;
                    rows[y] |= candidates;
                    blocks[block] |= candidates;
                }
            }

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var block = (x / 3) + (y / 3) * 3;
                    _candidates[x, y] = _values[x, y] != Value.None
                        ? Candidates.None
                        : Candidates.All ^ (cols[x] | rows[y] | blocks[block]);
                }
            }
        }

        public bool GetIsGiven(Position position)
        {
            return _isGivens[position.x, position.y];
        }

        public void SetIsGiven(Position position, bool value)
        {
            _isGivens[position.x, position.y] = value;
        }

        public Grid Clone()
        {
            return new Grid(
                (Value[,]) _values.Clone(),
                (Candidates[,]) _candidates.Clone(),
                (bool[,]) _isGivens.Clone()
            );
        }

        public bool HasValue(Position pos) => GetValue(pos) != Value.None;

        public Candidates GetCandidates(Position position) => _candidates[position.x, position.y];

        public void Restart()
        {
            foreach (var position in Position.Positions)
            {
                if (!GetIsGiven(position))
                {
                    SetValue(position, Value.None);
                }
            }

            ClearAllCandidates();
        }

        public bool IsValueLegal(Position position) => IsCandidateLegal(position, GetValue(position));

        public bool IsSudokuSolved()
        {
            return Position.Positions.All(position => HasValue(position) && IsValueLegal(position));
        }
    }
}
using System;
using System.Diagnostics;
using System.Linq;

namespace Weboku.Core.Data
{
    public sealed class Grid
    {
        private readonly Value[] _values;
        private readonly Candidates[] _candidates;
        private readonly bool[] _isGivens;

        public Grid()
        {
            _values = new Value[81];
            _candidates = new Candidates[81];
            _isGivens = new bool[81];
        }

        private Grid(Value[] inputs, Candidates[] candidates, bool[] isGivens)
        {
            _values = inputs;
            _candidates = candidates;
            _isGivens = isGivens;
        }

        public Value GetValue(in Position position) => _values[position.Index];

        public void SetValue(in Position position, Value value)
        {
            _values[position.Index] = value;
            _candidates[position.Index] = Candidates.None;

            if (value == Value.None) return;

            foreach (var seenBy in Position.GetPositionsSeenBy(position))
            {
                RemoveCandidate(seenBy, value);
            }
        }

        public bool IsCandidateLegal(Position position, Value value)
        {
            if (value == Value.None) return true;

            return Position.GetPositionsSeenBy(position)
                .Where(otherPosition => !position.Equals(otherPosition))
                .All(otherPosition => GetValue(otherPosition) != value);
        }

        public bool HasCandidate(in Position position, Value value)
            => (_candidates[position.Index] & value.AsCandidates()) == value.AsCandidates();

        public void ToggleCandidate(in Position position, Value value)
            => _candidates[position.Index] ^= value.AsCandidates();

        public void RemoveCandidate(in Position position, Value value)
            => _candidates[position.Index] &= ~value.AsCandidates();

        public void AddCandidate(in Position position, Value value)
            => _candidates[position.Index] |= value.AsCandidates();

        public void ClearAllCandidates()
        {
            foreach (var position in Position.Positions)
            {
                ClearCandidates(position);
            }
        }

        public void ClearCandidates(in Position position)
        {
            _candidates[position.Index] = Candidates.None;
        }

        public int GetCandidatesCount(in Position position)
        {
            var candidates = GetCandidates(position);
            return candidates.Count();
        }

        public void FillAllLegalCandidates()
        {
            var stopwatch = Stopwatch.StartNew();

            var cols = new Candidates[9];
            var rows = new Candidates[9];
            var blocks = new Candidates[9];
            foreach (var position in Position.Positions)
            {
                var candidates = _values[position.Index].AsCandidates();
                cols[position.X] |= candidates;
                rows[position.Y] |= candidates;
                blocks[position.Block] |= candidates;
            }

            foreach (var position in Position.Positions)
            {
                _candidates[position.Index] = _values[position.Index] != Value.None
                    ? Candidates.None
                    : Candidates.All ^ (cols[position.X] | rows[position.Y] | blocks[position.Block]);
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        public bool GetIsGiven(Position position)
        {
            return _isGivens[position.Index];
        }

        public void SetIsGiven(Position position, bool value)
        {
            _isGivens[position.Index] = value;
        }

        public Grid Clone()
        {
            return new(
                (Value[]) _values.Clone(),
                (Candidates[]) _candidates.Clone(),
                (bool[]) _isGivens.Clone()
            );
        }

        public bool HasValue(Position pos) => GetValue(pos) != Value.None;

        public Candidates GetCandidates(Position position) => _candidates[position.Index];

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
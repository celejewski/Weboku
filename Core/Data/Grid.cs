using Core.Helpers;
using System.Collections.Generic;
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

        public InputValue GetValue(Position pos) => _inputs[pos.x, pos.y];
        public void SetValue(Position pos, InputValue value)
        {
            _inputs[pos.x, pos.y] = value;
            _candidates[pos.x, pos.y] = Candidates.None;

            if( value != InputValue.None )
            {
                foreach( var seenBy in GridHelper.GetCoordsWhichCanSee(pos) )
                {
                    RemoveCandidate(seenBy, value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasCandidate(Position pos, InputValue value)
            => (_candidates[pos.x, pos.y] & value.AsCandidates()) == value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToggleCandidate(Position pos, InputValue value)
            => _candidates[pos.x, pos.y] ^= value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveCandidate(Position pos, InputValue value)
            => _candidates[pos.x, pos.y] &= ~value.AsCandidates();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCandidate(Position pos, InputValue value)
            => _candidates[pos.x, pos.y] |= value.AsCandidates();

        public void ClearAllCandidates()
        {
            foreach( var pos in Position.All )
            {
                ClearCandidates(pos);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearCandidates(Position pos)
        {
            _candidates[pos.x, pos.y] = Candidates.None;
        }

        public void FillAllLegalCandidates()
        {
            foreach( var pos in Position.All.Where(pos => !HasValue(pos)) )
            {
                _candidates[pos.x, pos.y] = Candidates.All;
            }

            foreach( var positionWithValue in Position.All.Where(HasValue) )
            {
                foreach( var pos in Position.GetOtherPositionsSeenBy(positionWithValue) )
                {
                    var value = GetValue(positionWithValue);
                    RemoveCandidate(pos, value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetIsGiven(Position pos)
        {
            return _isGivens[pos.x, pos.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIsGiven(Position pos, bool value)
        {
            _isGivens[pos.x, pos.y] = value;
        }

        public IGrid Clone()
        {
            return new Grid(
                (InputValue[,]) _inputs.Clone(),
                (Candidates[,]) _candidates.Clone(),
                (bool[,]) _isGivens.Clone()
                );
        }

        public int CandidatesCount(Position pos)
        {
            return _candidates[pos.x, pos.y].Count();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasValue(Position pos) => GetValue(pos) != InputValue.None;

        private readonly static Dictionary<Candidates, IReadOnlyList<InputValue>> _candidatesDict
    = new Dictionary<Candidates, IReadOnlyList<InputValue>>();
        public IReadOnlyList<InputValue> GetCandidatesWithCache(Position pos)
        {
            var key = _candidates[pos.x, pos.y];
            if( !_candidatesDict.ContainsKey(key) )
            {
                _candidatesDict[key] = InputValue.NonEmpty
                    .Where(value => HasCandidate(pos, value))
                    .ToList()
                    .AsReadOnly();
            }
            return _candidatesDict[key];
        }

        public Candidates GetCandidates(Position pos) => _candidates[pos.x, pos.y];

        public int GetGivensHashcode()
        {
            var values = Position.All.Select(pos => GetIsGiven(pos) ? GetValue(pos) : InputValue.None);
            return string.Join("", values).GetHashCode();
        }

        public override string ToString()
        {
            return "Grid";
        }
    }
}

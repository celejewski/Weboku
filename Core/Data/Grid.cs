using Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public InputValue GetValue(Position pos) => _inputs[pos.x, pos.y];
        public void SetValue(Position pos, InputValue value)
        {
            _inputs[pos.x, pos.y] = value;
            _candidates[pos.x, pos.y] = CandidateValue.None;

            if( value != InputValue.Empty )
            {
                foreach( var seenBy in GridHelper.GetCoordsWhichCanSee(pos) )
                {
                    RemoveCandidate(seenBy, value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasCandidate(Position pos, InputValue value) => (_candidates[pos.x, pos.y] & value.ToCandidateValue()) == value.ToCandidateValue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ToggleCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] ^= value.ToCandidateValue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] &= ~value.ToCandidateValue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] |= value.ToCandidateValue();


        public void ClearCandidates()
        {
            foreach( var pos in Position.All )
            {
                ClearCandidates(pos);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearCandidates(Position pos)
        {
            _candidates[pos.x, pos.y] = CandidateValue.None;
        }

        public void FillCandidates()
        {
            foreach( var pos in Position.All.Where(pos => !HasValue(pos)) )
            {
                _candidates[pos.x, pos.y] = CandidateValue.All;
            }

            foreach( var positionWithValue in Position.All.Where(pos => HasValue(pos)) )
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
                (CandidateValue[,]) _candidates.Clone(),
                (bool[,]) _isGivens.Clone()
                );
        }

        private static readonly Dictionary<CandidateValue, int> _candidatesCount = new Dictionary<CandidateValue, int>(1024);
        public int CandidatesCount(Position pos)
        {
            var candidates = _candidates[pos.x, pos.y];
            if( !_candidatesCount.ContainsKey(candidates) )
            {
                int count = 0;
                for( int value = 1; value < 10; value++ )
                {
                    if( HasCandidate(pos, value) )
                    {
                        count += 1;
                    }
                }
                _candidatesCount[candidates] = count;
            }
            var result = _candidatesCount[candidates];
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasValue(Position pos) => GetValue(pos) != InputValue.Empty;


        private readonly static Dictionary<CandidateValue, IReadOnlyList<InputValue>> _candidatesDict
    = new Dictionary<CandidateValue, IReadOnlyList<InputValue>>();
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

        public CandidateValue GetCandidates(Position pos) => _candidates[pos.x, pos.y];

        public int GetGivensHashcode()
        {
            var values = Position.All.Select(pos => GetIsGiven(pos) ? GetValue(pos) : InputValue.Empty);
            var hashcode = string.Join("", values).GetHashCode();
            return hashcode;
        }

        public override string ToString()
        {
            return "Grid";
        }
    }
}

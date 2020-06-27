﻿using Core.Helpers;
using System.Collections.Generic;

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
        }

        public bool HasCandidate(Position pos, InputValue value) => (_candidates[pos.x, pos.y] & value.ToCandidateValue()) == value.ToCandidateValue();
        public void ToggleCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] ^= value.ToCandidateValue();
        public void RemoveCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] &= ~value.ToCandidateValue();
        public void AddCandidate(Position pos, InputValue value) => _candidates[pos.x, pos.y] |= value.ToCandidateValue();


        public void ClearCandidates()
        {
            foreach( var pos in Position.All )
            {
                ClearCandidates(pos);
            }
        }

        public void ClearCandidates(Position pos)
        {
            _candidates[pos.x, pos.y] = CandidateValue.None;
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

        public bool GetIsGiven(Position pos)
        {
            return _isGivens[pos.x, pos.y];
        }

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

        public bool HasValue(Position pos) => GetValue(pos) != InputValue.Empty;

        public IEnumerable<InputValue> GetCandidates(Position pos)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                if (HasCandidate(pos, value))
                {
                    yield return value;
                }
            }
        }
    }
}

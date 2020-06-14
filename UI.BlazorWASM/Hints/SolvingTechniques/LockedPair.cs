using Core.Data;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedPair : ISolvingTechnique
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public LockedPair(Position pos1, Position pos2, InputValue value1, InputValue value2)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _value1 = value1;
            _value2 = value2;
        }

        public bool CanExecute(Informer informer)
        {
            return informer.WithCandidate(GetPositions(), _value1).Any()
                || informer.WithCandidate(GetPositions(), _value2).Any();
        }

        public void Display(Displayer displayer, Informer informer)
        {
            displayer.SetTitle("Locked pair");
            displayer.SetDescription($"Because the {_pos1} and {_pos2} cells have two and only two values of {_value1:D} and {_value2:D} this means that we can remove these values from the other cells that see both {_pos1} and {_pos2}." +
                $" If we insert the value of {_value1:D} or {_value2:D} in other cells, then {_pos1} or {_pos2} would be left without a valid value.");

            var posWithValue1 = informer.WithCandidate(GetPositions(), _value1);
            var posWithValue2 = informer.WithCandidate(GetPositions(), _value2);

            displayer.Mark(Enums.Color.Illegal, posWithValue1, _value1);
            displayer.Mark(Enums.Color.Illegal, posWithValue2, _value2);
            displayer.Mark(Enums.Color.Legal, GetPair(), _value1);
            displayer.Mark(Enums.Color.Legal, GetPair(), _value2);

            displayer.HighlightBlock(_pos1);
            displayer.HighlightHouse(_pos1, RowOrCol);
        }

        public void Execute(Executor executor)
        {
            executor.RemoveCandidates(_value1, GetPositions());
            executor.RemoveCandidates(_value2, GetPositions());
            executor.AddCandidates(_value1, GetPair());
            executor.AddCandidates(_value2, GetPair());
        }

        private House RowOrCol => _pos1.X == _pos2.X ? House.Col : House.Row;

        private IEnumerable<Position> GetPositions()
        {
            return HintsHelper.GetPositionsInBlock(_pos1)
                .Concat(HintsHelper.GetPositionsInHouse(_pos1, RowOrCol))
                .Except(GetPair());
        }

        private IEnumerable<Position> GetPair()
        {
                yield return _pos1;
                yield return _pos2;
        }
    }
}

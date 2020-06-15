using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedPair : ISolvingTechnique
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public NakedPair(Position pos1, Position pos2, InputValue value1, InputValue value2)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _value1 = value1;
            _value2 = value2;
        }

        public bool CanExecute(Informer informer)
        {
            return GetPositions(informer).Any();
        }

        public void Display(Displayer displayer, Informer informer)
        {
            var positionsWithPair = GetPair();
            var positionsWithCandidate = GetPositions(informer);

            displayer.SetTitle("Naked Pair");
            displayer.SetDescription($"Because the {_pos1} and {_pos2} cells have two and only two values of {_value1:D} and {_value2:D} this means that we can remove these values from the other cells that see both {_pos1} and {_pos2}." +
                $" If we insert the value of {_value1:D} or {_value2:D} in other cells, then {_pos1} or {_pos2} would be left without a valid value.");

            displayer.Mark(Enums.Color.Illegal, positionsWithCandidate, _value1);
            displayer.Mark(Enums.Color.Illegal, positionsWithCandidate, _value2);
            displayer.Mark(Enums.Color.Legal, positionsWithPair, _value1);
            displayer.Mark(Enums.Color.Legal, positionsWithPair, _value2);
            displayer.HighlightHouse(_pos1, GetHouse());
        }

        public void Execute(Executor executor, Informer informer)
        {
            var positions = HintsHelper.GetPositionsInHouse(_pos1, GetHouse());
            executor.RemoveCandidates(_value1, positions);
            executor.RemoveCandidates(_value2, positions);
            executor.AddCandidates(_value1, GetPair());
            executor.AddCandidates(_value2, GetPair());
        }

        private House GetHouse() => HintsHelper.RowOrCol(_pos1, _pos2);

        private IEnumerable<Position> GetPair()
        {
            yield return _pos1;
            yield return _pos2;
        }

        private IEnumerable<Position> GetPositions(Informer informer)
        {
            return HintsHelper.GetPositionsInHouse(_pos1, GetHouse())
                .Where(pos => informer.HasCandidate(pos, _value1) || informer.HasCandidate(pos, _value2))
                .Except(GetPair());
        }
    }
}

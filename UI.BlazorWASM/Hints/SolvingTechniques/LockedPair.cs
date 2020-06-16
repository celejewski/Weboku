using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedPair : NakedSubset
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public LockedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            : base(positions, values)
        {
            _pos1 = positions.First();
            _pos2 = positions.Last();
            _value1 = values.First();
            _value2 = values.Last();
        }


        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            displayer.SetTitle("Locked pair");
            displayer.SetDescription($"Because the {_pos1} and {_pos2} cells have two and only two values of {_value1:D} and {_value2:D} this means that we can remove these values from the other cells that see both {_pos1} and {_pos2}." +
                $" If we insert the value of {_value1:D} or {_value2:D} in other cells, then {_pos1} or {_pos2} would be left without a valid value.");
        }

    }
}

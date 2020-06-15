using Core.Data;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedPair : NakedSubset
    {
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value1;
        private readonly InputValue _value2;

        public LockedPair(Position pos1, Position pos2, InputValue value1, InputValue value2)
            : base(new[]{ pos1, pos2 }, new[]{value1, value2 })
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _value1 = value1;
            _value2 = value2;
        }


        public override void Display(Displayer displayer, Informer informer)
        {
            base.Display(displayer, informer);
            displayer.SetTitle("Locked pair");
            displayer.SetDescription($"Because the {_pos1} and {_pos2} cells have two and only two values of {_value1:D} and {_value2:D} this means that we can remove these values from the other cells that see both {_pos1} and {_pos2}." +
                $" If we insert the value of {_value1:D} or {_value2:D} in other cells, then {_pos1} or {_pos2} would be left without a valid value.");
        }

    }
}

using Core.Data;
using System;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class Skyscrapper : BaseSolvingTechnique
    {
        private readonly Position _base1;
        private readonly Position _base2;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _value;

        public Skyscrapper(Position base1, Position base2, Position pos1, Position pos2, InputValue value)
            :base("skyscrapper")
        {
            _base1 = base1;
            _base2 = base2;
            _pos1 = pos1;
            _pos2 = pos2;
            _value = value;
        }

        public override bool CanExecute(Informer informer)
        {
            return Position.GetOtherPositionsSeenBy(_pos1, _pos2)
                .Any(pos => informer.HasCandidate(pos, _value));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);

            displayer.Mark(Enums.Color.Legal, _pos1, _value);
            displayer.Mark(Enums.Color.Legal, _pos2, _value);
            displayer.Mark(Enums.Color.Legal, _base1, _value);
            displayer.Mark(Enums.Color.Legal, _base2, _value);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, Position.GetOtherPositionsSeenBy(_pos1, _pos2), _value);

            displayer.HighlightHouse(_base1, Position.GetHouse(_base1, _base2));
            displayer.HighlightHouse(_pos1, Position.GetHouse(_pos1, _base1));
            displayer.HighlightHouse(_pos2, Position.GetHouse(_pos2, _base2));

            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            executor.RemoveCandidates(_value, Position.GetOtherPositionsSeenBy(_pos1, _pos2));
        }
    }
}

using Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class XYWing : BaseSolvingTechnique
    {
        private readonly Position _pivot;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _candidate1;
        private readonly InputValue _candidate2;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly InputValue _value;

        public XYWing(Position pivot, Position pos1, Position pos2, InputValue candidate1, InputValue candidate2, IEnumerable<Position> positionsToRemove, InputValue value)
            : base("xywing")
        {
            _pivot = pivot;
            _pos1 = pos1;
            _pos2 = pos2;
            _candidate1 = candidate1;
            _candidate2 = candidate2;
            _positionsToRemove = positionsToRemove;
            _value = value;
        }


        public override bool CanExecute(Informer informer)
        {
            return _positionsToRemove.Any(pos => informer.HasCandidate(pos, _value));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            displayer.SetDescription(DescriptionKey, _pivot, _pos1, _pos2, _value);

            displayer.MarkCandidate(Enums.Color.Third, _pivot, _candidate1);
            displayer.Mark(Enums.Color.Third, _pos1, _value);
            displayer.MarkCandidate(Enums.Color.Fourth, _pos1, _candidate1);

            displayer.MarkCandidate(Enums.Color.Fourth, _pivot, _candidate2);
            displayer.Mark(Enums.Color.Fourth, _pos2, _value);
            displayer.MarkCandidate(Enums.Color.Third, _pos2, _candidate2); ;

            displayer.MarkCell(Enums.Color.Legal, _pivot);
            displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsToRemove, _value);
            displayer.SetValueFilter(_value);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            foreach( var pos in _positionsToRemove )
            {
                executor.RemoveCandidate(_value, pos);
            }
        }
    }
}

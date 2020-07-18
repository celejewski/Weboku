using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class XYWingDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _pivot;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly InputValue _candidate1;
        private readonly InputValue _candidate2;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly InputValue _value;

        public XYWingDisplayer(XYWing xyWing) 
            : base(xyWing, "xywing")
        {
            _pivot = xyWing.Pivot;
            _pos1 = xyWing.Pos1;
            _pos2 = xyWing.Pos2;
            _candidate1 = xyWing.Candidate1;
            _candidate2 = xyWing.Candidate2;
            _positionsToRemove = xyWing.PositionsToRemove;
            _value = xyWing.Value;
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
    }
}

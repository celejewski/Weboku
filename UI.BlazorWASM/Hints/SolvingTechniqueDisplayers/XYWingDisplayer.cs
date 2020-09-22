using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class XYWingDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly Position _pivot;
        private readonly Position _pos1;
        private readonly Position _pos2;
        private readonly Value _candidate1;
        private readonly Value _candidate2;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly Value _value;

        public XYWingDisplayer(Informer informer, Displayer displayer, XYWing xyWing) 
            : base(informer, displayer, xyWing, "xywing")
        {
            _pivot = xyWing.Pivot;
            _pos1 = xyWing.Pos1;
            _pos2 = xyWing.Pos2;
            _candidate1 = xyWing.Candidate1;
            _candidate2 = xyWing.Candidate2;
            _positionsToRemove = xyWing.PositionsToRemove;
            _value = xyWing.Value;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();
            _displayer.SetDescription(DescriptionKey, _pivot, _pos1, _pos2, _value);

            _displayer.MarkCandidate(Enums.Color.Third, _pivot, _candidate1);
            _displayer.Mark(Enums.Color.Third, _pos1, _value);
            _displayer.MarkCandidate(Enums.Color.Fourth, _pos1, _candidate1);

            _displayer.MarkCandidate(Enums.Color.Fourth, _pivot, _candidate2);
            _displayer.Mark(Enums.Color.Fourth, _pos2, _value);
            _displayer.MarkCandidate(Enums.Color.Third, _pos2, _candidate2); ;

            _displayer.MarkCell(Enums.Color.Legal, _pivot);
            _displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positionsToRemove, _value);
            _displayer.SetValueFilter(_value);
        }
    }
}

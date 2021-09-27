using System.Collections.Generic;
using Weboku.Application.Enums;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
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

            _displayer.MarkCandidate(Color.Third, _pivot, _candidate1);
            _displayer.Mark(Color.Third, _pos1, _value);
            _displayer.MarkCandidate(Color.Fourth, _pos1, _candidate1);

            _displayer.MarkCandidate(Color.Fourth, _pivot, _candidate2);
            _displayer.Mark(Color.Fourth, _pos2, _value);
            _displayer.MarkCandidate(Color.Third, _pos2, _candidate2);
            ;

            _displayer.MarkCell(Color.Legal, _pivot);
            _displayer.MarkIfHasCandidate(Color.Illegal, _positionsToRemove, _value);
            _displayer.SetValueFilter(_value);
        }
    }
}
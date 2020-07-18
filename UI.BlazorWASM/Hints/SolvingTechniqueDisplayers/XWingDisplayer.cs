using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class XWingDisplayer : BaseSolvingTechniqueDisplayer
    {
        private readonly InputValue _value;
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<Position> _positionsToRemove;
        private readonly House _house;

        public XWingDisplayer(XWing xWing)
            : base(xWing, "xwing")
        {
            _value = xWing.Value;
            _positions = xWing.Positions;
            _positionsToRemove = xWing.PositionsToRemove;
            _house = xWing.House;
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);

            var opositeHouse = _house == House.Col ? House.Row : House.Col;
            displayer.SetDescription(
                DescriptionKey,
                displayer.Format(_house, _positions.First()),
                displayer.Format(_house, _positions.Last()),
                _value,
                displayer.Format(opositeHouse, _positions.First()),
                displayer.Format(opositeHouse, _positions.Last())
                );
            displayer.SetValueFilter(_value);

            foreach( var pos in _positions )
            {
                displayer.HighlightCol(pos);
                displayer.HighlightRow(pos);
            }

            displayer.MarkCandidates(Enums.Color.Legal, _positions, _value);
            displayer.MarkCandidates(Enums.Color.Illegal, _positionsToRemove, _value);
        }
    }
}

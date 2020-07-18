using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class NakedSubsetDisplayer : BaseSolvingTechniqueDisplayer
    {

        private Position Pos => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<InputValue> _values;

        public NakedSubsetDisplayer(NakedSubset nakedSubset)
            : base(nakedSubset, "naked-subset")
        {
            _positions = nakedSubset.Positions;
            _values = nakedSubset.Values;
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            base.DisplaySolution(displayer, informer);
            foreach( var value in _values )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Illegal, GetPositionsToRemove(informer), value);
                displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            foreach( var house in GetHouses() )
            {
                displayer.HighlightHouse(Pos, house);
            }
            displayer.SetValueFilter(InputValue.Empty);
        }

        private IEnumerable<House> GetHouses()
        {
            return HintsHelper.GetHouses(_positions);
        }

        private IEnumerable<Position> GetPositionsToRemove(Informer informer)
        {
            var positionsInHouses = GetHouses()
                .SelectMany(house => HintsHelper.GetPositionsInHouse(Pos, house));

            return positionsInHouses
                .Where(pos => _values.Any(value => informer.HasCandidate(pos, value)))
                .Except(_positions);
        }
    }
}

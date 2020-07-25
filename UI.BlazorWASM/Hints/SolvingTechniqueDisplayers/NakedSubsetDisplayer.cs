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

        public NakedSubsetDisplayer(Informer informer, Displayer displayer, NakedSubset nakedSubset)
            : base(informer, displayer, nakedSubset, "naked-subset")
        {
            _positions = nakedSubset.Positions;
            _values = nakedSubset.Values;
        }

        public override void DisplaySolution()
        {
            base.DisplaySolution();
            foreach( var value in _values )
            {
                _displayer.MarkIfHasCandidate(Enums.Color.Illegal, GetPositionsToRemove(_informer), value);
                _displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            foreach( var house in GetHouses() )
            {
                _displayer.HighlightHouse(Pos, house);
            }
            _displayer.SetValueFilter(InputValue.Empty);
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

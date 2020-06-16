using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSubset : BaseSolvingTechnique
    {
        private Position Pos => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<InputValue> _values;

        public NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            :base("Naked subset")
        {
            _positions = positions;
            _values = values;
        }

        public override bool CanExecute(Informer informer)
        {
            return GetPositionsToRemove(informer).Any();
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
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

        public override void Execute(Executor executor, Informer informer)
        {
            foreach( var value in _values )
            {
                executor.RemoveCandidates(value, GetPositionsToRemove(informer));
            }
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

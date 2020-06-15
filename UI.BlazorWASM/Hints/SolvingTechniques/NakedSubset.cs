using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class NakedSubset : ISolvingTechnique
    {
        private Position Pos => _positions.First();
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<InputValue> _values;

        public NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            _positions = positions;
            _values = values;
        }

        public bool CanExecute(Informer informer)
        {
            return GetPositionsToRemove(informer).Any();
        }

        public virtual void Display(Displayer displayer, Informer informer)
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

        public void Execute(Executor executor, Informer informer)
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

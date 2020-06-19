using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSubset : BaseSolvingTechnique
    {
        protected Position Position => _positions.First();
        protected readonly IEnumerable<Position> _positions;
        protected readonly IEnumerable<InputValue> _values;
        protected readonly House _house;

        public HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            :base("Hidden subset")
        {
            _positions = positions;
            _values = values;
            _house = HintsHelper.GetHouses(positions).First();
        }

        public override bool CanExecute(Informer informer)
        {
            return ValuesToRemove().Any(value => _positions.Any(pos => informer.HasCandidate(pos, value)));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(_title);
            foreach( var value in ValuesToRemove() )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positions, value);
            }

            foreach( var value in _values )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            displayer.HighlightHouse(Position, _house);
            displayer.SetValueFilter(InputValue.Empty);
        }

        public override void Execute(Executor executor, Informer informer)
        {
            foreach( var value in ValuesToRemove() )
            {
                executor.RemoveCandidates(value, _positions);
            }
        }

        private IEnumerable<InputValue> ValuesToRemove()
        {
            for( int i = 1; i < 10; i++ )
            {
                var value = (InputValue) i;
                if (!_values.Contains(value))
                {
                    yield return value;
                }
            }
        }
    }
}

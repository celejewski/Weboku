using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSubset : BaseSolvingTechnique
    {
        private Position Pos => _positions.First();
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<InputValue> _values;

        public HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            :base("Hidden subset")
        {
            _positions = positions;
            _values = values;
        }

        public override bool CanExecute(Informer informer)
        {
            return ValuesToRemove().Any(value => _positions.Any(pos => informer.HasCandidate(pos, value)));
        }

        public override void DisplaySolution(Displayer displayer, Informer informer)
        {
            foreach( var value in ValuesToRemove() )
            {
                displayer.MarkIfHasCandidate(Enums.Color.Illegal, _positions, value);
            }

            foreach( var value in _values )
            {
                System.Console.WriteLine($"Legal {value}");
                displayer.MarkIfHasCandidate(Enums.Color.Legal, _positions, value);
            }

            foreach( var house in HintsHelper.GetHouses(_positions) )
            {
                displayer.HighlightHouse(Pos, house);
            }
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

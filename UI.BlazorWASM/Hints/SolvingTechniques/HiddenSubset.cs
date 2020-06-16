using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class HiddenSubset : ISolvingTechnique
    {
        private Position Pos => _positions.First();
        private readonly IEnumerable<Position> _positions;
        private readonly IEnumerable<InputValue> _values;

        public HiddenSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            _positions = positions;
            _values = values;
        }

        public bool CanExecute(Informer informer)
        {
            return ValuesToRemove().Any(value => _positions.Any(pos => informer.HasCandidate(pos, value)));
        }

        public virtual void Display(Displayer displayer, Informer informer)
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

            foreach( var house in GetHouses() )
            {
                displayer.HighlightHouse(Pos, house);
            }
            displayer.SetValueFilter(InputValue.Empty);
        }

        public void Execute(Executor executor, Informer informer)
        {
            foreach( var value in ValuesToRemove() )
            {
                executor.RemoveCandidates(value, _positions);
            }
        }

        private IEnumerable<House> GetHouses()
        {
            return HintsHelper.GetHouses(_positions);
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

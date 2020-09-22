using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.SolvingTechniques
{
    public class HiddenSubset : ISolvingTechnique
    {
        public Position Position => Positions.First();

        public IEnumerable<Position> Positions { get; }

        public IEnumerable<Value> Values { get; }

        public House House { get; }

        public HiddenSubset(IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            Positions = positions;
            Values = values;
            House = Position.GetHouses(positions).First();
        }

        public bool CanExecute(IGrid grid)
        {
            return ValuesToRemove()
                .Any(value => Positions.Any(pos => grid.HasCandidate(pos, value)));
        }

        public void Execute(IGrid grid)
        {
            foreach( var value in ValuesToRemove() )
            {
                foreach( var pos in Positions )
                {
                    grid.RemoveCandidate(pos, value);
                }
            }
        }

        public IEnumerable<Value> ValuesToRemove()
        {
            return Value.NonEmpty.Except(Values);
        }
    }
}

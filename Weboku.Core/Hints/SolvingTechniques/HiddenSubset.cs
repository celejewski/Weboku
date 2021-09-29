using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
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
            House = Position.GetHouseOf(positions);
        }

        public bool CanExecute(Grid grid)
        {
            return ValuesToRemove()
                .Any(value => Positions.Any(pos => grid.HasCandidate(pos, value)));
        }

        public void Execute(Grid grid)
        {
            foreach (var value in ValuesToRemove())
            {
                foreach (var pos in Positions)
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
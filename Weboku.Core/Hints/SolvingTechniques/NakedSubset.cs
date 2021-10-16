using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class NakedSubset : ISolvingTechnique
    {
        public NakedSubset(IEnumerable<Position> positions, IEnumerable<Value> values)
        {
            Positions = positions;
            Values = values;
        }

        public IEnumerable<Position> Positions { get; }

        public IEnumerable<Value> Values { get; }

        public bool CanExecute(Grid grid)
        {
            return GetPositionsToRemove(grid).Any();
        }

        public void Execute(Grid grid)
        {
            foreach (var value in Values)
            {
                foreach (var pos in GetPositionsToRemove(grid))
                {
                    grid.RemoveCandidate(pos, value);
                }
            }
        }

        private IEnumerable<Position> GetPositionsToRemove(Grid grid)
        {
            var positionsInHouses = Position.GetPositionsSeenByAll(Positions);

            return positionsInHouses
                .Where(pos => Values.Any(value => grid.HasCandidate(pos, value)))
                .Except(Positions);
        }
    }
}
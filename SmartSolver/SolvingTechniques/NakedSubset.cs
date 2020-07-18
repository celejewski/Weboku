using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class NakedSubset : ISolvingTechnique
    {
        public NakedSubset(IEnumerable<Position> positions, IEnumerable<InputValue> values)
        {
            Positions = positions;
            Values = values;
        }

        public IEnumerable<Position> Positions { get; }

        public IEnumerable<InputValue> Values { get; }

        public bool CanExecute(IGrid grid)
        {
            return GetPositionsToRemove(grid).Any();
        }

        public void Execute(IGrid grid)
        {
            foreach( var value in Values )
            {
                foreach( var pos in GetPositionsToRemove(grid) )
                {
                    grid.RemoveCandidate(pos, value);
                }
            }
        }


        private IEnumerable<Position> GetPositionsToRemove(IGrid grid)
        {
            var positionsInHouses = Position.GetOtherPositionsSeenBy(Positions);

            return positionsInHouses
                .Where(pos => Values.Any(value => grid.HasCandidate(pos, value)))
                .Except(Positions);
        }
    }
}

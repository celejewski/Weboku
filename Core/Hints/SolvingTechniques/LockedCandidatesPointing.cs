using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.SolvingTechniques
{
    public class LockedCandidatesPointing : ISolvingTechnique
    {
        public int Block { get; }
        public Value Value { get; }
        public IEnumerable<Position> PositionsToRemoveFrom { get; }

        public LockedCandidatesPointing(int block, Value value, IEnumerable<Position> positionsToRemoveFrom)
        {
            Block = block;
            Value = value;
            PositionsToRemoveFrom = positionsToRemoveFrom;
        }

        public bool CanExecute(Grid grid)
        {
            return PositionsToRemoveFrom.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(Grid grid)
        {
            foreach( var pos in PositionsToRemoveFrom )
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}

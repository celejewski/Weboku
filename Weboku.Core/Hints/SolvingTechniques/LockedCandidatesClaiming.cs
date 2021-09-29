using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class LockedCandidatesClaiming : ISolvingTechnique
    {
        public LockedCandidatesClaiming(Value value, IEnumerable<Position> positionsToRemoveCandidate, House house)
        {
            this.Value = value;
            PositionsToRemoveCandidate = positionsToRemoveCandidate;
            House = house;
        }

        public IEnumerable<Position> PositionsToRemoveCandidate { get; }

        public House House { get; }

        public Value Value { get; }

        public bool CanExecute(Grid grid)
        {
            return PositionsToRemoveCandidate.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(Grid grid)
        {
            foreach (var pos in PositionsToRemoveCandidate)
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}
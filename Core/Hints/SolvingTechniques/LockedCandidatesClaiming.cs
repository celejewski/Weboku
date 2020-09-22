using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.SolvingTechniques
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

        public bool CanExecute(IGrid grid)
        {
            return PositionsToRemoveCandidate.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in PositionsToRemoveCandidate )
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}

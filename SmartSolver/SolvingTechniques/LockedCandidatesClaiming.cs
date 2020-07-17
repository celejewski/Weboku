using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class LockedCandidatesClaiming : ISolvingTechnique
    {
        public LockedCandidatesClaiming(InputValue inputValue, IEnumerable<Position> positionsToRemoveCandidate, House house)
        {
            InputValue = inputValue;
            PositionsToRemoveCandidate = positionsToRemoveCandidate;
            House = house;

        }

        public IEnumerable<Position> PositionsToRemoveCandidate { get; }

        public House House { get; }

        public InputValue InputValue { get; }

        public bool CanExecute(IGrid grid)
        {
            return PositionsToRemoveCandidate.Any(pos => grid.HasCandidate(pos, InputValue));
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in PositionsToRemoveCandidate)
            {
                grid.RemoveCandidate(pos, InputValue);
            }
        }
    }
}

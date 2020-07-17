using Core.Data;
using Core.Helpers;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class LockedCandidatesClaimingFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public LockedCandidatesClaimingFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var house in Position.Rows.Concat(Position.Cols) )
                {
                    var positionsInHouse = house.WithCandidate(grid, value);
                    var positionsToRemove = Position.GetOtherPositionsSeenBy(positionsInHouse)
                        .WithCandidate(grid, value);

                    if( positionsToRemove.Any() )
                    {
                        yield return _factory.LockedCandidatesClaiming(value, positionsToRemove, Position.GetHouse(positionsInHouse));
                    }
                }
            }
        }
    }
}

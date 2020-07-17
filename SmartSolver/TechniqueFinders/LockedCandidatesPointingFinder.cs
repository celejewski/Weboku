using Core.Data;
using Core.Helpers;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class LockedCandidatesPointingFinder : BaseHiddenSubsetFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public LockedCandidatesPointingFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var block in Position.Blocks )
                {
                    var positionsInBlock = block.WithCandidate(grid, value);
                    var positionsToRemove = Position.GetOtherPositionsSeenBy(positionsInBlock)
                        .WithCandidate(grid, value);

                    if( positionsInBlock.Any() )
                    {
                        var first = positionsInBlock.First();
                        yield return _factory.LockedCandidatesPointing(first.block, value, positionsToRemove);
                    }
                    
                }
            }
        }
    }
}

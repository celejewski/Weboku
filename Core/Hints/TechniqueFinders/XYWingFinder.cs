using Core.Data;
using Core.Hints.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class XYWingFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            var pairPositions = Position.All.Where(pos => grid.GetCandidates(pos).Count() == 2);

            foreach( var pivot in pairPositions )
            {
                var seenBy = Position.GetCoordsWhichCanSee(pivot)
                    .Where(pos => grid.GetCandidates(pos).Count() == 2);

                var candidate1 = grid.GetCandidates(pivot).ToValues()[0];
                var candidate2 = grid.GetCandidates(pivot).ToValues()[1];

                var seenWithCandidate1 = seenBy.Where(pos => grid.HasCandidate(pos, candidate1) && !grid.HasCandidate(pos, candidate2));
                var seenWithCandidate2 = seenBy.Where(pos => grid.HasCandidate(pos, candidate2) && !grid.HasCandidate(pos, candidate1));

                foreach( var pos1 in seenWithCandidate1.Where(pos => !pos.Equals(pivot)) )
                {
                    foreach( var pos2 in seenWithCandidate2.Where(pos => !pos.Equals(pivot) && !pos.Equals(pos1)) )
                    {
                        if( pos1.IsSharingHouseWith(pos2) ) continue;

                        var candidates1 = grid.GetCandidates(pos1);
                        var candidates2 = grid.GetCandidates(pos2);
                        var sharedValue = candidates1.ToValues().FirstOrDefault(value => candidates2.ToValues().Contains(value));
                        if( sharedValue == Value.None ) continue;

                        var positionsToRemoveFrom = Position.GetOtherPositionsSeenBy(pos1, pos2)
                            .Where(pos => grid.HasCandidate(pos, sharedValue));

                        if( positionsToRemoveFrom.Any() )
                        {
                            yield return new XYWing(pivot, pos1, pos2, candidate1, candidate2, positionsToRemoveFrom, sharedValue);
                        }
                    }
                }
            }
        }
    }
}

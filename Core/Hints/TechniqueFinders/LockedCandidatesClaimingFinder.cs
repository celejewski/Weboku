﻿using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class LockedCandidatesClaimingFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                var (cols, rows, blocks, blockXcols, blockXrows)
                    = HintsHelper.GetCandidatesCountEx(grid, value);

                for( int block = 0; block < 9; block++ )
                {
                    for( int i = 0; i < 3; i++ )
                    {
                        var col = (block % 3) * 3 + i;
                        if( cols[col] > 0 && blockXcols[block, col] == cols[col] && blocks[block] > cols[col] )
                        {
                            var positionsToRemove = Position.Blocks[block]
                                .Where(pos => grid.HasCandidate(pos, value) && pos.x != col);
                            yield return new LockedCandidatesClaiming(value, positionsToRemove, House.Col);
                        }

                        var row = (block / 3) * 3 + i;
                        if( rows[row] > 0 && blockXrows[block, row] == rows[row] && blocks[block] > rows[row] )
                        {
                            var positionsToRemove = Position.Blocks[block]
                                .Where(pos => grid.HasCandidate(pos, value) && pos.y != row);
                            yield return new LockedCandidatesClaiming(value, positionsToRemove, House.Row);
                        }
                    }
                }
            }
        }
    }
}
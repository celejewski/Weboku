﻿using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class LockedCandidatesPointingFinder : HiddenSubsetFinderBase
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
                        if( blocks[block] > 0
                            && cols[col] > blockXcols[block, col]
                            && blocks[block] == blockXcols[block, col] )
                        {
                            var positionsToRemove = Position.Cols[col].Where(pos => pos.block != block).ToList();
                            yield return new LockedCandidatesPointing(block, value, positionsToRemove);
                        }

                        var row = (block / 3) * 3 + i;
                        if( blocks[block] > 0
                            && rows[row] > blockXrows[block, row]
                            && blocks[block] == blockXrows[block, row] )
                        {
                            var positionsToRemove = Position.Rows[row].Where(pos => pos.block != block).ToList();
                            yield return new LockedCandidatesPointing(block, value, positionsToRemove);
                        }
                    }
                }
            }
        }
    }
}
using Core.Data;
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
                var cols = new int[9];
                var rows = new int[9];
                var blocks = new int[9];
                var blockXcols = new int[9, 9];
                var blockXrows = new int[9, 9];

                foreach( var pos in Position.All )
                {
                    if( !grid.HasCandidate(pos, value) ) continue;

                    cols[pos.x]++;
                    rows[pos.y]++;
                    blocks[pos.block]++;
                    blockXcols[pos.block, pos.x]++;
                    blockXrows[pos.block, pos.y]++;
                }

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

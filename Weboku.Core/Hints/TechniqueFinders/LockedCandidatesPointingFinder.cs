using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class LockedCandidatesPointingFinder : HiddenSubsetFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var value in Value.NonEmpty)
            {
                var (cols, rows, blocks, blockXcols, blockXrows)
                    = HintsHelper.GetCandidatesCountEx(grid, value);

                for (int block = 0; block < 9; block++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var col = (block % 3) * 3 + i;
                        if (blocks[block] > 0
                            && cols[col] > blockXcols[block, col]
                            && blocks[block] == blockXcols[block, col])
                        {
                            var positionsToRemove = Position.Cols[col].Where(pos => pos.Block != block).ToList();
                            yield return new LockedCandidatesPointing(block, value, positionsToRemove);
                        }

                        var row = (block / 3) * 3 + i;
                        if (blocks[block] > 0
                            && rows[row] > blockXrows[block, row]
                            && blocks[block] == blockXrows[block, row])
                        {
                            var positionsToRemove = Position.Rows[row].Where(pos => pos.Block != block).ToList();
                            yield return new LockedCandidatesPointing(block, value, positionsToRemove);
                        }
                    }
                }
            }
        }
    }
}
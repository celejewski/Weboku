using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class TwoStringKiteFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach( var value in Value.NonEmpty )
            {
                var count = grid.GetCandidatesCount(value);

                for( int i = 0; i < 9; i++ )
                {
                    if( count.blocks[i] != 2 ) continue;

                    var positionsInBlock = Position.Blocks[i].Where(pos => grid.HasCandidate(pos, value));
                    var pos1 = positionsInBlock.First();
                    var pos2 = positionsInBlock.Last();

                    if( pos1.x == pos2.x || pos1.y == pos2.y ) continue;

                    if( count.cols[pos1.x] == 2 && count.rows[pos2.y] == 2 )
                    {
                        if( MakeTwoStringKiteOrDefault(grid, value, pos1, pos2) is TwoStringKite kite )
                        {
                            yield return kite;
                        }
                    }

                    if( count.rows[pos1.y] == 2 && count.cols[pos2.x] == 2 )
                    {
                        if( MakeTwoStringKiteOrDefault(grid, value, pos2, pos1) is TwoStringKite kite )
                        {
                            yield return kite;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public TwoStringKite MakeTwoStringKiteOrDefault(Grid grid, Value value, Position posInCol, Position posInRow)
        {
            var legalInCol = Position.Cols[posInCol.x]
                .Where(pos => grid.HasCandidate(pos, value))
                .Single(pos => !pos.Equals(posInCol));

            var legalInRow = Position.Rows[posInRow.y]
                .Where(pos => grid.HasCandidate(pos, value))
                .Single(pos => !pos.Equals(posInRow));

            var posToRemove = Position.Cols[legalInRow.x][legalInCol.y];

            return grid.HasCandidate(posToRemove, value)
                ? new TwoStringKite(value, new[] { legalInCol, legalInRow }, new[] { posInCol, posInRow }, posToRemove)
                : null;
        }
    }
}

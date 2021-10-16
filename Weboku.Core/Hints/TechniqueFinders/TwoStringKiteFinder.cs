using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class TwoStringKiteFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var value in Value.NonEmpty)
            {
                var count = grid.GetCandidatesCount(value);

                for (int i = 0; i < 9; i++)
                {
                    if (count.blocks[i] != 2) continue;

                    var positionsInBlock = Position.Blocks[i].Where(pos => grid.HasCandidate(pos, value));
                    var pos1 = positionsInBlock.First();
                    var pos2 = positionsInBlock.Last();

                    if (pos1.X == pos2.X || pos1.Y == pos2.Y) continue;

                    if (count.cols[pos1.X] == 2 && count.rows[pos2.Y] == 2)
                    {
                        if (MakeTwoStringKiteOrDefault(grid, value, pos1, pos2) is TwoStringKite kite)
                        {
                            yield return kite;
                        }
                    }

                    if (count.rows[pos1.Y] == 2 && count.cols[pos2.X] == 2)
                    {
                        if (MakeTwoStringKiteOrDefault(grid, value, pos2, pos1) is TwoStringKite kite)
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
            var legalInCol = Position.Cols[posInCol.X]
                .Where(pos => grid.HasCandidate(pos, value))
                .Single(pos => !pos.Equals(posInCol));

            var legalInRow = Position.Rows[posInRow.Y]
                .Where(pos => grid.HasCandidate(pos, value))
                .Single(pos => !pos.Equals(posInRow));

            var posToRemove = Position.Cols[legalInRow.X][legalInCol.Y];

            return grid.HasCandidate(posToRemove, value)
                ? new TwoStringKite(value, new[] {legalInCol, legalInRow}, new[] {posInCol, posInRow}, posToRemove)
                : null;
        }
    }
}
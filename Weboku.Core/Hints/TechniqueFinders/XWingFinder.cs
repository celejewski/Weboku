using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class XWingFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var value in Value.NonEmpty)
            {
                var (cols, rows, blocks) = grid.GetCandidatesCount(value);

                for (int i = 0; i < 9; i++)
                {
                    if (cols[i] != 2) continue;

                    for (int j = i + 1; j < 9; j++)
                    {
                        if (cols[j] != 2) continue;

                        var col1 = Position.Cols[i].Where(pos => grid.HasCandidate(pos, value));
                        var col2 = Position.Cols[j].Where(pos => grid.HasCandidate(pos, value));

                        var col1_1 = col1.First();
                        var col1_2 = col1.Last();
                        var col2_1 = col2.First();
                        var col2_2 = col2.Last();

                        var positions = col1.Concat(col2).ToList();

                        if ((col1_1.y, col1_2.y) == (col2_1.y, col2_2.y))
                        {
                            var positionsToRemove = Position.GetOtherPositionsSeenBy(col1_1, col2_1)
                                .Concat(Position.GetOtherPositionsSeenBy(col1_2, col2_2))
                                .Where(pos => grid.HasCandidate(pos, value))
                                .Except(positions);

                            yield return new XWing(value, positions, positionsToRemove, House.Col);
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    if (rows[i] != 2) continue;

                    for (int j = i + 1; j < 9; j++)
                    {
                        if (rows[j] != 2) continue;

                        var row1 = Position.Rows[i].Where(pos => grid.HasCandidate(pos, value));
                        var row2 = Position.Rows[j].Where(pos => grid.HasCandidate(pos, value));

                        var row1_1 = row1.First();
                        var row1_2 = row1.Last();
                        var row2_1 = row2.First();
                        var row2_2 = row2.Last();

                        var positions = row1.Concat(row2).ToList();

                        if ((row1_1.x, row1_2.x) == (row2_1.x, row2_2.x))
                        {
                            var positionsToRemove = Position.GetOtherPositionsSeenBy(row1_1, row2_1)
                                .Concat(Position.GetOtherPositionsSeenBy(row1_2, row2_2))
                                .Where(pos => grid.HasCandidate(pos, value))
                                .Except(positions);

                            yield return new XWing(value, positions, positionsToRemove, House.Row);
                        }
                    }
                }
            }
        }
    }
}
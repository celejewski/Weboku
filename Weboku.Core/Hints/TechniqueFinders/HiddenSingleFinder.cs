using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class HiddenSingleFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            var rowsFoundAlready = new Candidates[9];
            var colsFoundAlready = new Candidates[9];
            var blocksFoundAlready = new Candidates[9];
            var rowsLeft = new Candidates[9];
            var colsLeft = new Candidates[9];
            var blocksLeft = new Candidates[9];
            for (int i = 0; i < 9; i++)
            {
                rowsLeft[i] = Candidates.All;
                colsLeft[i] = Candidates.All;
                blocksLeft[i] = Candidates.All;
            }

            for (int i = 0; i < 81; i++)
            {
                var pos = Position.Positions[i];
                var candidates = grid.GetCandidates(pos);

                colsLeft[pos.X] &= ~(colsFoundAlready[pos.X] & candidates);
                colsFoundAlready[pos.X] |= candidates;
                rowsLeft[pos.Y] &= ~(rowsFoundAlready[pos.Y] & candidates);
                rowsFoundAlready[pos.Y] |= candidates;
                blocksLeft[pos.Block] &= ~(blocksFoundAlready[pos.Block] & candidates);
                blocksFoundAlready[pos.Block] |= candidates;
            }

            var results = new List<ISolvingTechnique>(10);
            for (int i = 0; i < 9; i++)
            {
                CheckResult(grid, results, House.Block, Position.Blocks[i], blocksLeft[i] & blocksFoundAlready[i]);
                CheckResult(grid, results, House.Col, Position.Cols[i], colsLeft[i] & colsFoundAlready[i]);
                CheckResult(grid, results, House.Row, Position.Rows[i], rowsLeft[i] & rowsFoundAlready[i]);
            }

            return results;
        }

        private void CheckResult(Grid grid, List<ISolvingTechnique> results, House house, IReadOnlyList<Position> pos, Candidates candidates)
        {
            foreach (var value in candidates.ToValues())
            {
                var position = pos.First(pos => grid.HasCandidate(pos, value));
                results.Add(new HiddenSingle(position, value, house));
            }
        }
    }
}
using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class HiddenSingleFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            var rowsFoundAlready = new Candidates[9];
            var colsFoundAlready = new Candidates[9];
            var blocksFoundAlready = new Candidates[9];
            var rowsLeft = new Candidates[9];
            var colsLeft = new Candidates[9];
            var blocksLeft = new Candidates[9];
            for( int i = 0; i < 9; i++ )
            {
                rowsLeft[i] = Candidates.All;
                colsLeft[i] = Candidates.All;
                blocksLeft[i] = Candidates.All;
            }

            for( int i = 0; i < 81; i++ )
            {
                var pos = Position.All[i];
                var candidates = grid.GetCandidates(pos);

                colsLeft[pos.x] &= ~(colsFoundAlready[pos.x] & candidates);
                colsFoundAlready[pos.x] |= candidates;
                rowsLeft[pos.y] &= ~(rowsFoundAlready[pos.y] & candidates);
                rowsFoundAlready[pos.y] |= candidates;
                blocksLeft[pos.block] &= ~(blocksFoundAlready[pos.block] & candidates);
                blocksFoundAlready[pos.block] |= candidates;
            }

            var results = new List<ISolvingTechnique>(10);
            for( int i = 0; i < 9; i++ )
            {
                CheckResult(grid, results, House.Block, Position.Blocks[i], blocksLeft[i] & blocksFoundAlready[i]);
                CheckResult(grid, results, House.Col, Position.Cols[i], colsLeft[i] & colsFoundAlready[i]);
                CheckResult(grid, results, House.Row, Position.Rows[i], rowsLeft[i] & rowsFoundAlready[i]);
            }
            return results;
        }

        private void CheckResult(IGrid grid, List<ISolvingTechnique> results, House house, List<Position> pos, Candidates candidates)
        {
            foreach( var value in candidates.ToInputValues() )
            {
                var position = pos.Find(pos => grid.HasCandidate(pos, value));
                results.Add(new HiddenSingle(position, value, house));
            }
        }
    }
}

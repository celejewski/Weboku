using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public class HiddenSingleFinderOld : TechniqueFinderBase
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
                rowsFoundAlready[i] = Candidates.None;
                colsFoundAlready[i] = Candidates.None;
                blocksFoundAlready[i] = Candidates.None;
                rowsLeft[i] = Candidates.All;
                colsLeft[i] = Candidates.All;
                blocksLeft[i] = Candidates.All;
            }

            for( int i = 0; i < Position.All.Count; i++ )
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

            for( int i = 0; i < 9; i++ )
            {
                var candidatesBlock = blocksLeft[i] & blocksFoundAlready[i];
                foreach( var value in candidatesBlock.ToInputValues() )
                {
                    var position = Position.Blocks[i].Find(pos => grid.HasCandidate(pos, value));
                    yield return new HiddenSingle(position, value, House.Block);
                }

                var candidatesCol = colsLeft[i] & colsFoundAlready[i];
                foreach( var value in candidatesCol.ToInputValues() )
                {
                    var position = Position.Cols[i].Find(pos => grid.HasCandidate(pos, value));
                    yield return new HiddenSingle(position, value, House.Col);
                }

                var candidatesRow = rowsLeft[i] & rowsFoundAlready[i];
                foreach( var value in candidatesRow.ToInputValues() )
                {
                    var position = Position.Rows[i].Find(pos => grid.HasCandidate(pos, value));
                    yield return new HiddenSingle(position, value, House.Row);
                }
            }
        }
    }
}

using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class FullHouseFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            var cols = new byte[9];
            var rows = new byte[9];
            var blocks = new byte[9];
            for( int i = 0; i < 81; i++ )
            {
                var pos = Position.All[i];
                if( grid.HasValue(pos) )
                {
                    cols[pos.x]++;
                    rows[pos.y]++;
                    blocks[pos.block]++;
                }
            }

            var items = new List<ISolvingTechnique>();
            for( int i = 0; i < 9; i++ )
            {
                if( blocks[i] == 8 ) items.Add(FindMissingValue(grid, Position.Blocks[i]));
                if( cols[i] == 8 ) items.Add(FindMissingValue(grid, Position.Cols[i]));
                if( rows[i] == 8 ) items.Add(FindMissingValue(grid, Position.Rows[i]));
            }
            return items;
        }

        private FullHouse FindMissingValue(IGrid grid, IReadOnlyList<Position> positions)
        {
            var candidates = Candidates.All;
            Position position = default;
            foreach( var pos in positions )
            {
                if( !grid.HasValue(pos) ) position = pos;
                candidates ^= grid.GetValue(pos).AsCandidates();
            }
            return new FullHouse(position, candidates.ToValues().Single());
        }
    }
}

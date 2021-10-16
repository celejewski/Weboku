using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class FullHouseFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            var cols = new byte[9];
            var rows = new byte[9];
            var blocks = new byte[9];
            for (int i = 0; i < 81; i++)
            {
                var pos = Position.Positions[i];
                if (grid.HasValue(pos))
                {
                    cols[pos.X]++;
                    rows[pos.Y]++;
                    blocks[pos.Block]++;
                }
            }

            var items = new List<ISolvingTechnique>();
            for (int i = 0; i < 9; i++)
            {
                if (blocks[i] == 8) items.Add(FindMissingValue(grid, Position.Blocks[i]));
                if (cols[i] == 8) items.Add(FindMissingValue(grid, Position.Cols[i]));
                if (rows[i] == 8) items.Add(FindMissingValue(grid, Position.Rows[i]));
            }

            return items;
        }

        private FullHouse FindMissingValue(Grid grid, IReadOnlyList<Position> positions)
        {
            var candidates = Candidates.All;
            Position position = default;
            foreach (var pos in positions)
            {
                if (!grid.HasValue(pos)) position = pos;
                candidates ^= grid.GetValue(pos).AsCandidates();
            }

            return new FullHouse(position, candidates.ToValues().Single());
        }
    }
}
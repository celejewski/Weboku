using Weboku.Core.Data;

namespace Weboku.Core.Hints
{
    public static class HintsHelper
    {
        public static (int[] cols, int[] rows, int[] blocks)
            GetCandidatesCount(this Grid grid, Value value)
        {
            var cols = new int[9];
            var rows = new int[9];
            var blocks = new int[9];

            foreach (var pos in Position.Positions)
            {
                if (grid.HasCandidate(pos, value))
                {
                    cols[pos.x]++;
                    rows[pos.y]++;
                    blocks[pos.block]++;
                }
            }

            return (cols, rows, blocks);
        }

        public static (int[] cols, int[] rows, int[] blocks, int[,] blockXcols, int[,] blockXrows)
            GetCandidatesCountEx(this Grid grid, Value value)
        {
            var cols = new int[9];
            var rows = new int[9];
            var blocks = new int[9];
            var blockXcols = new int[9, 9];
            var blockXrows = new int[9, 9];

            foreach (var pos in Position.Positions)
            {
                if (grid.HasCandidate(pos, value))
                {
                    cols[pos.x]++;
                    rows[pos.y]++;
                    blocks[pos.block]++;

                    blockXcols[pos.block, pos.x]++;
                    blockXrows[pos.block, pos.y]++;
                }
            }

            return (cols, rows, blocks, blockXcols, blockXrows);
        }
    }
}
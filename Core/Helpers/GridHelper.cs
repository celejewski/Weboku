﻿using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public static class GridHelper
    {
        private static readonly IReadOnlyList<Position>[,] _indexesWhichCanSee = new IReadOnlyList<Position>[9, 9];

        public static IReadOnlyList<Position> GetCoordsWhichCanSee(Position pos)
        {
            return _indexesWhichCanSee[pos.x, pos.y] ??= CalculateIndexesWhichCanSee(pos);
        }

        private static IReadOnlyList<Position> CalculateIndexesWhichCanSee(Position pos)
        {
            return Position.Cols[pos.x]
                .Concat(Position.Rows[pos.y])
                .Concat(Position.Blocks[pos.block])
                .ToArray();
        }

        public static bool IsLegal(Position pos, InputValue value, IGrid grid)
        {
            return value == InputValue.None
                || GetCoordsWhichCanSee(pos)
                .Where(coords => !pos.Equals(coords))
                .All(coords => grid.GetValue(coords) != value);
        }

        public static (int[] cols, int[] rows, int[] blocks)
            GetCandidatesCount(this IGrid grid, InputValue value)
        {
            var cols = new int[9];
            var rows = new int[9];
            var blocks = new int[9];

            foreach( var pos in Position.All )
            {
                if( grid.HasCandidate(pos, value) )
                {
                    cols[pos.x]++;
                    rows[pos.y]++;
                    blocks[pos.block]++;
                }
            }

            return (cols, rows, blocks);
        }

        public static (int[] cols, int[] rows, int[] blocks, int[,] blockXcols, int[,] blockXrows)
            GetCandidatesCountEx(this IGrid grid, InputValue value)
        {
            var cols = new int[9];
            var rows = new int[9];
            var blocks = new int[9];
            var blockXcols = new int[9, 9];
            var blockXrows = new int[9, 9];

            foreach( var pos in Position.All )
            {
                if( grid.HasCandidate(pos, value) )
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

        public static (bool[,] valueXcols, bool[,] valueXrows, bool[,] valueXblocks)
            GetContainsCandidates(this IGrid grid)
        {
            var valueXcols = new bool[10, 9];
            var valueXrows = new bool[10, 9];
            var valueXblocks = new bool[10, 9];

            foreach( var pos in Position.All )
            {
                var value = grid.GetValue(pos);
                valueXcols[value, pos.x] = true;
                valueXrows[value, pos.y] = true;
                valueXblocks[value, pos.block] = true;
            }

            return (valueXcols, valueXrows, valueXblocks);
        }
    }
}

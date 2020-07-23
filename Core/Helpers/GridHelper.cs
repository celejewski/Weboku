using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            return value == InputValue.Empty
                || GetCoordsWhichCanSee(pos)
                .Where(coords => !pos.Equals(coords))
                .All(coords => grid.GetValue(coords) != value);
        }

        public static void SetAllLegalCandidates(IGrid grid)
        {
            grid.FillCandidates();
            foreach( var pos in Position.All )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    if( !IsLegal(pos, value, grid) )
                    {
                        grid.RemoveCandidate(pos, value);
                    }
                }
            }
        }

        public static void RemoveCandidatesSeenBy(IGrid grid, Position pos)
        {
            var value = grid.GetValue(pos);
            if (value == InputValue.Empty)
            {
                return;
            }

            foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos) )
            {
                grid.RemoveCandidate(coords, value);
            }
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
                    cols[pos.x] += 1;
                    rows[pos.y] += 1;
                    blocks[pos.block] += 1;
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
                    cols[pos.x] += 1;
                    rows[pos.y] += 1;
                    blocks[pos.block] += 1;

                    blockXcols[pos.block, pos.x] += 1;
                    blockXrows[pos.block, pos.y] += 1;
                }
            }

            return (cols, rows, blocks, blockXcols, blockXrows);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => Position.Rows[position.y],
                House.Col => Position.Cols[position.x],
                House.Block => Position.Blocks[position.block],
                _ => throw new ArgumentException("Unknown house"),
            };
        }

        private static readonly IEnumerable<House> _houses = new[] {House.Block, House.Row, House.Col};

        public static House HouseFirstOrDefault(Position pos, Predicate<IEnumerable<Position>> predicate)
        {
            return _houses.FirstOrDefault(house => predicate(GetPositionsInHouse(pos, house)));
        }

        public static IEnumerable<House> GetHouses(IEnumerable<Position> positions)
        {
            var first = positions.First();

            if (positions.All(pos => pos.x == first.x))
            {
                yield return House.Col;
            }

            if (positions.All(pos => pos.y == first.y))
            {
                yield return House.Row;
            }

            if (positions.All(pos => pos.block == first.block))
            {
                yield return House.Block;
            }
        }
    }
}
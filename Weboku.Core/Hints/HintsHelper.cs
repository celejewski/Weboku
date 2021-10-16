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
                    cols[pos.X]++;
                    rows[pos.Y]++;
                    blocks[pos.Block]++;
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
                    cols[pos.X]++;
                    rows[pos.Y]++;
                    blocks[pos.Block]++;

                    blockXcols[pos.Block, pos.X]++;
                    blockXrows[pos.Block, pos.Y]++;
                }
            }

            return (cols, rows, blocks, blockXcols, blockXrows);
        }

        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => Position.Rows[position.Y],
                House.Col => Position.Cols[position.X],
                House.Block => Position.Blocks[position.Block],
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

            if (positions.All(pos => pos.X == first.X))
            {
                yield return House.Col;
            }

            if (positions.All(pos => pos.Y == first.Y))
            {
                yield return House.Row;
            }

            if (positions.All(pos => pos.Block == first.Block))
            {
                yield return House.Block;
            }
        }
    }
}
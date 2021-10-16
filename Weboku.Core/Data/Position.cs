using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Exceptions;

namespace Weboku.Core.Data
{
    public readonly struct Position
    {
        public readonly int x;
        public readonly int y;
        public readonly int block;
        public readonly int index;

        private Position(int x, int y)
        {
            this.x = x;
            this.y = y;
            block = (y / 3) * 3 + (x / 3);
            index = y * 9 + x;
        }

        public override string ToString() => $"r{y + 1}c{x + 1}";

        public static Position TopLeftCornerOfBlock(int block)
        {
            var x = block % 3 * 3;
            var y = block / 3 * 3;

            return new Position(x, y);
        }

        public static IReadOnlyList<Position> Positions { get; }
        public static IReadOnlyList<IReadOnlyList<Position>> Rows { get; }
        public static IReadOnlyList<IReadOnlyList<Position>> Cols { get; }
        public static IReadOnlyList<IReadOnlyList<Position>> Blocks { get; }
        public static IReadOnlyList<IReadOnlyList<Position>> Houses { get; }

        public static IEnumerable<Position> GetOtherPositionsSeenBy(IEnumerable<Position> positions)
        {
            foreach (var position1 in Positions)
            {
                if (positions.All(position2 => position1.IsSharingHouseWith(position2)))
                {
                    yield return position1;
                }
            }
        }

        public static IEnumerable<Position> GetOtherPositionsSeenBy(params Position[] positions)
            => GetOtherPositionsSeenBy((IEnumerable<Position>) positions);

        public bool IsSharingHouseWith(Position position)
        {
            return x == position.x
                   || y == position.y
                   || block == position.block;
        }

        public static House GetHouseOf(params Position[] positions)
            => GetHouseOf((IEnumerable<Position>) positions);


        public static House GetHouseOf(IEnumerable<Position> positions)
        {
            if (!positions.Any())
            {
                throw new SudokuCoreException($"Can not use {nameof(GetHouseOf)} with empty {nameof(positions)}.");
            }

            var first = positions.First();
            if (positions.All(position => position.x == first.x)) return House.Col;
            if (positions.All(position => position.y == first.y)) return House.Row;
            if (positions.All(position => position.block == first.block)) return House.Block;
            return House.None;
        }

        private static readonly IReadOnlyList<Position>[,] _indexesWhichCanSeePosition = new IReadOnlyList<Position>[9, 9];

        public static IReadOnlyList<Position> GetCoordsWhichCanSeePosition(Position position)
        {
            return _indexesWhichCanSeePosition[position.x, position.y] ??= CalculateIndexesWhichCanSeePosition(position);
        }

        private static IReadOnlyList<Position> CalculateIndexesWhichCanSeePosition(Position position)
        {
            return Cols[position.x]
                .Concat(Rows[position.y])
                .Concat(Blocks[position.block])
                .Distinct()
                .ToArray();
        }

        static Position()
        {
            var positions = new List<Position>();
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var position = new Position(x, y);
                    positions.Add(position);
                    Console.WriteLine(position.index);
                }
            }

            Positions = positions;

            var rows = new List<List<Position>>();
            var cols = new List<List<Position>>();
            var blocks = new List<List<Position>>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new List<Position>());
                cols.Add(new List<Position>());
                blocks.Add(new List<Position>());
            }

            foreach (var position in Positions)
            {
                cols[position.x].Add(position);
                rows[position.y].Add(position);
                blocks[position.block].Add(position);
            }

            Rows = rows;
            Cols = cols;
            Blocks = blocks;

            Houses = Blocks
                .Concat(Cols)
                .Concat(Rows)
                .ToList();
        }

        public override int GetHashCode() => index;

        public override bool Equals(object obj)
        {
            if (obj is Position pos) return Equals(pos);
            return false;
        }

        public bool Equals((int x, int y) position)
        {
            return position == (x, y);
        }

        public bool Equals(Position other) => x == other.x && y == other.y;

        public static IEnumerable<House> GetHouses(IEnumerable<Position> positions)
        {
            var first = positions.First();
            if (positions.All(position => position.x == first.x)) yield return House.Col;
            if (positions.All(position => position.y == first.y)) yield return House.Row;
            if (positions.All(position => position.block == first.block)) yield return House.Block;
        }
    }
}
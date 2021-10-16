using System;
using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Exceptions;

namespace Weboku.Core.Data
{
    public readonly struct Position
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Block;
        public readonly int Index;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Block = (y / 3) * 3 + (x / 3);
            Index = y * 9 + x;
        }

        public override string ToString() => $"r{Y + 1}c{X + 1}";

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

        public static IEnumerable<Position> GetPositionsSeenByAll(IEnumerable<Position> positions)
        {
            foreach (var position1 in Positions)
            {
                if (positions.All(position2 => position1.IsSharingHouseWith(position2)))
                {
                    yield return position1;
                }
            }
        }

        public static IEnumerable<Position> GetPositionsSeenByAll(params Position[] positions)
            => GetPositionsSeenByAll((IEnumerable<Position>) positions);

        public bool IsSharingHouseWith(Position position)
        {
            return X == position.X
                   || Y == position.Y
                   || Block == position.Block;
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
            if (positions.All(position => position.X == first.X)) return House.Col;
            if (positions.All(position => position.Y == first.Y)) return House.Row;
            if (positions.All(position => position.Block == first.Block)) return House.Block;
            return House.None;
        }

        private static readonly IReadOnlyList<Position>[,] IndexesWhichCanSeePosition = new IReadOnlyList<Position>[9, 9];

        public static IReadOnlyList<Position> GetPositionsSeenBy(Position position)
        {
            return IndexesWhichCanSeePosition[position.X, position.Y] ??= CalculateIndexesWhichCanSeePosition(position);
        }

        private static IReadOnlyList<Position> CalculateIndexesWhichCanSeePosition(Position position)
        {
            return Cols[position.X]
                .Concat(Rows[position.Y])
                .Concat(Blocks[position.Block])
                .Where(p => !p.Equals(position))
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
                    Console.WriteLine(position.Index);
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
                cols[position.X].Add(position);
                rows[position.Y].Add(position);
                blocks[position.Block].Add(position);
            }

            Rows = rows;
            Cols = cols;
            Blocks = blocks;

            Houses = Blocks
                .Concat(Cols)
                .Concat(Rows)
                .ToList();
        }

        public override int GetHashCode() => Index;

        public override bool Equals(object obj)
        {
            if (obj is Position pos) return Equals(pos);
            return false;
        }

        public bool Equals((int x, int y) position)
        {
            return position == (X, Y);
        }

        public bool Equals(Position other) => X == other.X && Y == other.Y;
    }
}
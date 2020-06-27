using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public readonly struct Position
    {
        public readonly int x;
        public readonly int y;
        public readonly int block;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
            block = (y / 3) * 3 + (x / 3);
        }
        public override string ToString() => $"r{y + 1}c{x + 1}";

        public static Position FromBlock(int block)
        {
            var x = block % 3 * 3;
            var y = block / 3 * 3;

            return new Position(x, y);
        }

        private static readonly List<Position> _all = new List<Position>();
        public static IEnumerable<Position> All => _all;

        private static readonly List<List<Position>> _rows = new List<List<Position>>();
        public static IReadOnlyList<IEnumerable<Position>> Rows => _rows;

        private static readonly List<List<Position>> _cols = new List<List<Position>>();
        public static IReadOnlyList<IEnumerable<Position>> Cols => _cols;

        private static readonly List<List<Position>> _blocks = new List<List<Position>>();
        public static IReadOnlyList<IEnumerable<Position>> Blocks => _blocks;

        private static readonly List<IEnumerable<Position>> _houses;
        public static IEnumerable<IEnumerable<Position>> Houses => _houses;

        public static IEnumerable<Position> GetPositionsSeenBy(IEnumerable<Position> positions)
        {
            if (!positions.Any())
            {
                yield break;
            }

            var first = positions.First();
            if (positions.All(pos => pos.x == first.x))
            {
                foreach( var pos in Cols[first.x] )
                {
                    yield return pos;
                }
            }

            if (positions.All(pos => pos.y == first.y))
            {
                foreach( var pos in Rows[first.y] )
                {
                    yield return pos;
                }
            }

            if (positions.All(pos => pos.block == first.block))
            {
                foreach( var pos in Blocks[first.block] )
                {
                    yield return pos;
                }
            }
        }
        static Position()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    _all.Add(new Position(x, y));
                }
            }

            for( int i = 0; i < 9; i++ )
            {
                _rows.Add(new List<Position>());
                _cols.Add(new List<Position>());
                _blocks.Add(new List<Position>());
            }

            foreach( var pos in All )
            {
                _rows[pos.y].Add(pos);
                _cols[pos.x].Add(pos);
                _blocks[pos.block].Add(pos);
            }

            _houses = new List<IEnumerable<Position>>(
                Blocks
                .Concat(Cols)
                .Concat(Rows)
                );
        }

        public override int GetHashCode()
        {
            return y * 9 + x;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position pos)
            {
                return x == pos.x && y == pos.y;
            }
            return false;
        }
    }
}

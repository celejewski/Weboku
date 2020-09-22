using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public readonly struct Position
    {
        public readonly int x;
        public readonly int y;
        public readonly int block;

        private Position(int x, int y)
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
        public static IReadOnlyList<Position> All => _all;

        private static readonly List<List<Position>> _rows = new List<List<Position>>();
        public static IReadOnlyList<List<Position>> Rows => _rows;

        private static readonly List<List<Position>> _cols = new List<List<Position>>();
        public static IReadOnlyList<List<Position>> Cols => _cols;

        private static readonly List<List<Position>> _blocks = new List<List<Position>>();
        public static IReadOnlyList<List<Position>> Blocks => _blocks;

        private static readonly List<List<Position>> _houses;
        public static IReadOnlyList<List<Position>> Houses => _houses;

        public static IEnumerable<Position> GetOtherPositionsSeenBy(IEnumerable<Position> positions)
        {
            if( !positions.Any() ) yield break;

            var first = positions.First();
            if( positions.All(position => position.x == first.x) )
            {
                foreach( var posposition in Cols[first.x].Except(positions) )
                {
                    yield return posposition;
                }
            }

            if( positions.All(position => position.y == first.y) )
            {
                foreach( var position in Rows[first.y].Except(positions) )
                {
                    yield return position;
                }
            }

            if( positions.All(position => position.block == first.block) )
            {
                foreach( var position in Blocks[first.block].Except(positions) )
                {
                    yield return position;
                }
            }
        }

        public static IEnumerable<Position> GetOtherPositionsSeenBy(params Position[] positions)
        {
            foreach( var position1 in All )
            {
                if( positions.All(position2 => position1.IsSharingHouseWith(position2)) )
                {
                    yield return position1;
                }
            }
        }

        public bool IsSharingHouseWith(Position position)
        {
            return x == position.x
                || y == position.y
                || block == position.block;
        }

        public static House GetHouseOf(params Position[] positions) => GetHouseOf((IEnumerable<Position>) positions);
        public static House GetHouseOf(IEnumerable<Position> positions)
        {
            if( !positions.Any() ) return House.None;

            var first = positions.First();
            if( positions.All(position => position.x == first.x) ) return House.Col;
            if( positions.All(position => position.y == first.y) ) return House.Row;
            if( positions.All(position => position.block == first.block) ) return House.Block;
            return House.None;
        }

        private static readonly IReadOnlyList<Position>[,] _indexesWhichCanSee = new IReadOnlyList<Position>[9, 9];

        public static IReadOnlyList<Position> GetCoordsWhichCanSee(Position position)
        {
            return _indexesWhichCanSee[position.x, position.y] ??= CalculateIndexesWhichCanSee(position);
        }

        private static IReadOnlyList<Position> CalculateIndexesWhichCanSee(Position position)
        {
            return Cols[position.x]
                .Concat(Rows[position.y])
                .Concat(Blocks[position.block])
                .Distinct()
                .ToArray();
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

            foreach( var position in All )
            {
                _rows[position.y].Add(position);
                _cols[position.x].Add(position);
                _blocks[position.block].Add(position);
            }

            _houses = new List<List<Position>>(
                Blocks
                .Concat(Cols)
                .Concat(Rows)
                );
        }

        public override int GetHashCode() => y * 9 + x;

        public override bool Equals(object obj)
        {
            if( obj is Position pos ) return Equals(pos);
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
            if( positions.All(position => position.x == first.x) ) yield return House.Col;
            if( positions.All(position => position.y == first.y) ) yield return House.Row;
            if( positions.All(position => position.block == first.block) ) yield return House.Block;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class GridHelper
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
                .Concat(Position.Blocks[pos.Block])
                .ToArray();
        }


        public static IEnumerable<Position> GetIndexesFromRow(int y)
        {
            for( int col = 0; col < 9; col++ )
            {
                yield return new Position(col, y);
            }
        }

        public static IEnumerable<Position> GetIndexesFromCol(int x)
        {
            for( int row = 0; row < 9; row++ )
            {
                yield return new Position(x, row);
            }
        }

        public static IEnumerable<Position> GetIndexesFromBlock(int x, int y)
        {
            var blockX = (x / 3) * 3;
            var blockY = (y / 3) * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    var targetX = blockX + offsetX;
                    var targetY = blockY + offsetY;
                    yield return new Position(targetX, targetY);
                }
            }
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

        public static IEnumerable<InputValue> Values = new InputValue[]
        {
            InputValue.One,
            InputValue.Two,
            InputValue.Three,
            InputValue.Four,
            InputValue.Five,
            InputValue.Six,
            InputValue.Seven,
            InputValue.Eight,
            InputValue.Nine,
        };

        private static readonly List<Position> _positions = new List<Position>();
        public static IEnumerable<Position> Positions
        {
            get
            {
                if( _positions != null && _positions.Count > 0)
                {
                    return _positions;
                }
                
                for( int x = 0; x < 9; x++ )
                {
                    for( int y = 0; y < 9; y++ )
                    {
                        _positions.Add(new Position(x, y));
                    }
                };
                return _positions;
            }
        }
    }
}

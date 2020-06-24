using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class GridHelper
    {
        private static readonly IList<Position>[,] _indexesWhichCanSee = new IList<Position>[9, 9];
        private static readonly Dictionary<short, bool> _candidatesCount = new Dictionary<short, bool>();

        public static IList<Position> GetCoordsWhichCanSee(int x, int y)
        {
            return _indexesWhichCanSee[x, y] ??= CalculateIndexesWhichCanSee(x, y);
        }

        private static IList<Position> CalculateIndexesWhichCanSee(int x, int y)
        {
            return GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y))
                .ToList();
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

        public static bool IsLegal(int x, int y, InputValue value, IGrid grid)
        {
            return value == InputValue.Empty
                || GetCoordsWhichCanSee(x, y)
                .Where(coords => coords.X != x || coords.Y != y)
                .All(coords => grid.GetValue(coords.X, coords.Y) != value);
        }

        public static void SetAllLegalCandidates(IGrid grid)
        {
            grid.FillCandidates();
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int value = 1; value < 10; value++ )
                    {
                        if( !GridHelper.IsLegal(x, y, (InputValue) value, grid) )
                        {
                            grid.RemoveCandidate(x, y, (InputValue) value);
                        }
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

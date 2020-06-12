using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Helpers
{
    public class GridHelper
    {
        public static IList<Coords> GetCoordsWhichCanSee(int x, int y)
        {
            return _indexesWhichCanSee[x, y] ??= CalculateIndexesWhichCanSee(x, y);
        }

        private static readonly IList<Coords>[,] _indexesWhichCanSee = new IList<Coords>[9, 9];

        private static IList<Coords> CalculateIndexesWhichCanSee(int x, int y)
        {
            return GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y))
                .ToList();
        }

        private static IEnumerable<Coords> GetIndexesFromRow(int y)
        {
            for( int col = 0; col < 9; col++ )
            {
                yield return new Coords(col, y);
            }
        }

        private static IEnumerable<Coords> GetIndexesFromCol(int x)
        {
            for( int row = 0; row < 9; row++ )
            {
                yield return new Coords(x, row);
            }
        }

        private static IEnumerable<Coords> GetIndexesFromBlock(int x, int y)
        {
            var blockX = (x / 3) * 3;
            var blockY = (y / 3) * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    var targetX = blockX + offsetX;
                    var targetY = blockY + offsetY;
                    yield return new Coords(targetX, targetY);
                }
            }
        }
    }
}

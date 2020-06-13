﻿using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Helpers
{
    public class GridHelper
    {
        public static bool IsLegal(int x, int y, InputValue value, IGridProvider gridProvider)
        {
            return value == InputValue.Empty 
                || GetCoordsWhichCanSee(x, y)
                .Where(coords => coords.X != x || coords.Y != y)
                .All(coords => gridProvider.GetValue(coords.X, coords.Y) != value);
        }

        public static IList<Position> GetCoordsWhichCanSee(int x, int y)
        {
            return _indexesWhichCanSee[x, y] ??= CalculateIndexesWhichCanSee(x, y);
        }

        private static readonly IList<Position>[,] _indexesWhichCanSee = new IList<Position>[9, 9];

        private static IList<Position> CalculateIndexesWhichCanSee(int x, int y)
        {
            return GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y))
                .ToList();
        }

        private static IEnumerable<Position> GetIndexesFromRow(int y)
        {
            for( int col = 0; col < 9; col++ )
            {
                yield return new Position(col, y);
            }
        }

        private static IEnumerable<Position> GetIndexesFromCol(int x)
        {
            for( int row = 0; row < 9; row++ )
            {
                yield return new Position(x, row);
            }
        }

        private static IEnumerable<Position> GetIndexesFromBlock(int x, int y)
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
    }
}

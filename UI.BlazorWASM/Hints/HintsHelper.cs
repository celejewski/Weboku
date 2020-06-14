using Core.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class HintsHelper
    {
        private readonly IGridProvider _gridProvider;

        public HintsHelper(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">For example "r1c6"</param>
        /// <returns>For "r1c6" will return x=5, y=0</returns>
        public static Position GetCoords(string text)
        {
            var x = GetDigit(text, 3) - 1;
            var y = GetDigit(text, 1) - 1;
            return new Position(x, y);
        }

        public static int GetDigit(string text, int pos)
        {
            return int.Parse(text.Substring(pos, 1));
        }

        public static int GetDigit(char c)
        {
            return GetDigit(c.ToString(), 0);
        }

        public static int GetIndex(string text, int pos)
        {
            return GetDigit(text, pos) - 1;
        }

        public static int GetIndex(char c)
        {
            return GetDigit(c) - 1;
        }

        public static InputValue GetValue(string text, int pos)
        {
            return (InputValue) GetDigit(text, pos);
        }

        public static IEnumerable<Position> GetCellsInRow(int y)
        {

            for( int x = 0; x < 9; x++ )
            {
                yield return new Position(x, y);
            }
        }
        public static IEnumerable<Position> GetCellsInCol(int x)
        {

            for( int y = 0; y < 9; y++ )
            {
                yield return new Position(x, y);
            }
        }

        public static IEnumerable<Position> GetCellsInBlock(int x, int y)
        {
            var startingX = x / 3 * 3;
            var startingY = y / 3 * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    yield return new Position(startingX + offsetX, startingY + offsetY);
                }
            }
        }

        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => GetCellsInRow(position.Y),
                House.Col => GetCellsInCol(position.X),
                House.Block => GetCellsInBlock(position.X, position.Y),
            };
        }

        public static int GetBlock(int x, int y)
        {
            return y / 3 * 3 + x / 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">r1c136, r569c7</param>
        /// <returns></returns>
        public static IEnumerable<Position> GetPositions(string text)
        {
            var splited = text[1..].Split('c');
            var rows = splited[0].Select(t => GetIndex(t));
            var cols = splited[1].Select(t => GetIndex(t));

            foreach( var col in cols )
            {
                foreach( var row in rows )
                {
                    yield return new Position(col, row);
                }
            }
        }
    }
}

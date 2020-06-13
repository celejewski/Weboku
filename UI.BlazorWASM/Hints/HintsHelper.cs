using Core.Data;
using System.Collections.Generic;
using System.Linq;
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

        public static InputValue GetValue(string text, int pos)
        {
            return (InputValue) GetDigit(text, pos);
        }

        public IEnumerable<Position> GetCellsInRow(int y)
        {

            for( int x = 0; x < 9; x++ )
            {
                yield return new Position(x, y);
            }
        }
        public IEnumerable<Position> GetCellsInCol(int x)
        {

            for( int y = 0; y < 9; y++ )
            {
                yield return new Position(x, y);
            }
        }

        public IEnumerable<Position> GetCellsInBlock(int x, int y)
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

        public int GetCandidatesCountInRow(int y, int value)
        {
            return GetCellsInRow(y)
                .Count(coords => _gridProvider.HasCandidate(coords.X, coords.Y, (InputValue) value));
            ;
        }
        public int GetCandidatesCountInCol(int x, int value)
        {
            return GetCellsInCol(x)
               .Count(coords => _gridProvider.HasCandidate(coords.X, coords.Y, (InputValue) value));

        }
        public int GetCandidatesCountInBlock(int x, int y, int value)
        {
            return GetCellsInBlock(x, y)
              .Count(coords => _gridProvider.HasCandidate(coords.X, coords.Y, (InputValue) value));

        }

        public static int GetBlock(int x, int y)
        {
            return y / 3 * 3 + x / 3;
        }
    }
}

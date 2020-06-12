//using Core.Data;
//using Core.Generators;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using UI.BlazorWASM.Providers;

//namespace UI.BlazorWASM.Hints
//{
//    public class HintsHelper
//    {
//        private readonly ISudokuProvider _sudokuProvider;

//        public HintsHelper(ISudokuProvider sudokuProvider)
//        {
//            _sudokuProvider = sudokuProvider;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="text">For example "r1c6"</param>
//        /// <returns>For "r1c6" will return x=5, y=0</returns>
//        public static (int x, int y) GetCoords(string text)
//        {
//            var x = GetDigit(text, 3) - 1;
//            var y = GetDigit(text, 1) - 1;
//            return (x, y);
//        }

//        public static int GetDigit(string text, int pos)
//        {
//            return int.Parse(text.Substring(pos, 1));
//        }

//        public IEnumerable<ICell> GetCellsInRow(int y)
//        {

//            for( int x = 0; x < 9; x++ )
//            {
//                yield return _sudokuProvider.Cells[x, y];
//            }
//        }
//        public IEnumerable<ICell> GetCellsInCol(int x)
//        {

//            for( int y = 0; y < 9; y++ )
//            {
//                yield return _sudokuProvider.Cells[x, y];
//            }
//        }

//        public IEnumerable<ICell> GetCellsInBlock(int x, int y)
//        {
//            var startingX = x / 3 * 3;
//            var startingY = y / 3 * 3;
//            for( int offsetX = 0; offsetX < 3; offsetX++ )
//            {
//                for( int offsetY = 0; offsetY < 3; offsetY++ )
//                {
//                    yield return _sudokuProvider.Cells[startingX + offsetX, startingY + offsetY];
//                }
//            }
//        }

//        public int GetCandidatesCountInRow(int y, int value)
//        {
//            return GetCellsInRow(y)
//                .Count(cell => cell.Candidates.ContainsKey(value));
//        }
//        public int GetCandidatesCountInCol(int x, int value)
//        {
//            return GetCellsInCol(x)
//                .Count(cell => cell.Candidates.ContainsKey(value));
//        }
//        public int GetCandidatesCountInBlock(int x, int y, int value)
//        {
//            return GetCellsInBlock(x, y)
//                .Count(cell => cell.Candidates.ContainsKey(value));
//        }

//        public static int GetBlock(int x, int y)
//        {
//            return y / 3 * 3 + x / 3;
//        }
//    }
//}

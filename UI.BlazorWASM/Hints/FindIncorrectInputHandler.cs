using System;
using System.Collections.Generic;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class FindIncorrectInputHandler : HintHandler
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly ICellColorProvider _cellColorProvider;

        public FindIncorrectInputHandler(ISudokuProvider sudokuProvider, ICellColorProvider cellColorProvider)
        {
            _sudokuProvider = sudokuProvider;
            _cellColorProvider = cellColorProvider;
        }

        public override void Execute(string step, IEnumerator<string> enumerator)
        {
            Console.WriteLine(step + " FindIncorrectInput");
            bool allCorrect = true;
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    var value = _sudokuProvider.Cells[x, y].Input.Value;
                    var solution = int.Parse(_sudokuProvider.Sudoku.Solution[y * 9 + x].ToString());
                    if( value != 0 && solution != value )
                    {
                        _cellColorProvider.SetColor(x, y, Enums.CellColor.Illegal);
                        allCorrect = false;
                    }
                }
            }

            if (allCorrect)
            {
                _next?.Execute(step, enumerator);
            }
        }
    }
}

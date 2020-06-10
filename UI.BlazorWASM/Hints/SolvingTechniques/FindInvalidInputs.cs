using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindInvalidInputs : ISolvingTechnique
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly ICellColorProvider _cellColorProvider;

        public FindInvalidInputs(ISudokuProvider sudokuProvider, ICellColorProvider cellColorProvider)
        {
            _sudokuProvider = sudokuProvider;
            _cellColorProvider = cellColorProvider;
        }
        public string Name => "Remove Invalid Inputs";

        public string Desc => "There are some invalid inputs.";

        public bool CanExecute(ISudokuProvider sudokuProvider)
        {
            return GetInvalidCells().Any();
        }

        public void Display(HintsProvider hintsProvider)
        {
            foreach( var cell in GetInvalidCells() )
            {
                _cellColorProvider.SetColor(cell.X, cell.Y, Enums.Color.Illegal);
            }
        }

        public void Execute(ISudokuProvider sudokuProvider)
        {
            foreach( var cell in GetInvalidCells() )
            {
                _sudokuProvider.SetValue(cell.X, cell.Y, 0);
            }
        }

        private IEnumerable<ICell> GetInvalidCells()
        {

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var solutionDigit = int.Parse(_sudokuProvider.Sudoku.Solution.Substring(y * 9 + x, 1));
                    var cell = _sudokuProvider.Cells[x, y];
                    if( cell.Input.Value != 0 && cell.Input.Value != solutionDigit )
                    {
                        yield return cell;
                    }
                }
            }
        }
    }
}

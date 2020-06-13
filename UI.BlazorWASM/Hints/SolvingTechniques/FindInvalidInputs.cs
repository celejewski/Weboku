using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindInvalidInputs : ISolvingTechnique
    {
        private readonly SudokuProvider _sudokuProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridProvider _gridProvider;

        public FindInvalidInputs(SudokuProvider sudokuProvider, CellColorProvider cellColorProvider, IGridProvider gridProvider)
        {
            _sudokuProvider = sudokuProvider;
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }
        public string Name => "Remove Invalid Inputs";

        public string Desc => "There are some invalid inputs.";

        public bool CanExecute()
        {
            return GetInvalidCellCoords().Any();
        }

        public void Display()
        {
            foreach( var cell in GetInvalidCellCoords() )
            {
                _cellColorProvider.SetColor(cell.X, cell.Y, Enums.Color.Illegal);
            }
        }

        public void Execute()
        {
            foreach( var coords in GetInvalidCellCoords() )
            {
                _gridProvider.SetValue(coords.X, coords.Y, 0);
            }
        }

        private IEnumerable<Coords> GetInvalidCellCoords()
        {

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var solutionDigit = int.Parse(_sudokuProvider.Solution.Substring(y * 9 + x, 1));
                    if( _gridProvider.GetValue(x, y) != InputValue.Empty 
                        && _gridProvider.GetValue(x, y) != (InputValue) solutionDigit )
                    {
                        yield return new Coords(x, y);
                    }
                }
            }
        }
    }
}

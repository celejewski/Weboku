using System;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindCandidates : ISolvingTechnique
    {
        public string Name => "Add Missing Candidates";

        public string Desc => "There are some candidates missing.";

        public bool CanExecute(ISudokuProvider sudokuProvider)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var solutionDigit = int.Parse(sudokuProvider.Sudoku.Solution.Substring(y * 9 + x, 1));
                    var cell = sudokuProvider.Cells[x, y];
                    if(cell.Input.Value == 0 && !cell.Candidates.ContainsKey(solutionDigit))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Display(HintsProvider hintsProvider)
        {
        }

        public void Execute(ISudokuProvider sudokuProvider)
        {
            sudokuProvider.FillAllCandidates();
        }
    }
}

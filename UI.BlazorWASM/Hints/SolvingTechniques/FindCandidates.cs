using Core.Data;
using System;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class FindCandidates : ISolvingTechnique
    {
        private readonly SudokuProvider _sudokuProvider;
        private readonly IGridProvider _gridProvider;

        public FindCandidates(SudokuProvider sudokuProvider, IGridProvider gridProvider)
        {
            _sudokuProvider = sudokuProvider;
            _gridProvider = gridProvider;
        }

        public string Name => "Add Missing Candidates";

        public string Desc => "There are some candidates missing.";

        public bool CanExecute()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var solutionDigit = int.Parse(_sudokuProvider.Solution.Substring(y * 9 + x, 1));
                    if( !_gridProvider.HasValue(x, y) && !_gridProvider.HasCandidate(x, y, (InputValue) solutionDigit) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Display()
        {
        }

        public void Execute()
        {
            _gridProvider.FillAllLegalCandidates();
        }
    }
}

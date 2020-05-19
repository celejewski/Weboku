using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public class AddMissingCandidates : HintHandler
    {
        private readonly ISudokuProvider _sudokuProvider;

        public AddMissingCandidates(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
        }

        public override void Execute(string step, IEnumerator<string> enumerator)
        {
            bool candidatesAreMissing = false;
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    var cell = _sudokuProvider.Cells[x, y];
                    var solution = int.Parse(_sudokuProvider.Sudoku.Solution[y * 9 + x].ToString());
                    if (cell.Input.Value == 0 && !cell.Candidates.ContainsKey(solution))
                    {
                        candidatesAreMissing = true;
                    }
                }
            }

            if( candidatesAreMissing )
            {
                Console.WriteLine("There are candidates missing");
            }
            else
            {
                _next?.Execute(step, enumerator);
            }
        }
    }
}

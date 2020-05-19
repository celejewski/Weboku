using System;
using System.Linq;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Hints
{
    public class HintProvider
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IFilterProvider _filterProvider;
        private HintHandler _chain;

        public HintProvider(ISudokuProvider sudokuProvider, ICellColorProvider cellColorProvider, IFilterProvider filterProvider)
        {
            _sudokuProvider = sudokuProvider;
            _cellColorProvider = cellColorProvider;
            _filterProvider = filterProvider;
            SetChainOfCommand();
        }


        private void SetChainOfCommand()
        {
            var firstSolvingTechnique = new PrintStepHandler();

            _chain = new FindIncorrectInputHandler(_sudokuProvider, _cellColorProvider);

            

            _chain.SetNext(new AddMissingCandidates(_sudokuProvider))
                .SetNext(firstSolvingTechnique)
                .SetNext(new PrintTechniqueNameHandler())
                .SetNext(new NakedSingle(_sudokuProvider, _cellColorProvider, _filterProvider))
                .SetNext(new IterateStepsHandler())
                .SetNext(firstSolvingTechnique);
            ;
        }

        public void ShowHint()
        {
            var steps = _sudokuProvider.Sudoku.Steps;
            _chain.Execute(steps.First(), steps.GetEnumerator());
        }
    }
}

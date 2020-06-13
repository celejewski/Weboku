using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider 
    {
        private readonly Informer _informer;
        private readonly Displayer _displayer;
        private readonly Executor _executor;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly SudokuProvider _sudokuProvider;
        private readonly HodokuParser _parser = new HodokuParser();
        private IEnumerable<ISolvingTechnique> Techniques
        {
            get
            {
                yield return new FillMissingCandidates();
                foreach( var technique in _parser.GetSolvingTechniques(_sudokuProvider.Steps) )
                {
                    yield return technique;
                }
                yield return new NotFound();
            }
        }

        private ISolvingTechnique NextTechnique => Techniques.First(t => t.CanExecute(_informer));

        public HintsProvider(SudokuProvider sudokuProvider, Informer informer, Displayer displayer, Executor executor, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _informer = informer;
            _displayer = displayer;
            _executor = executor;
            _gridHistoryManager = gridHistoryManager;
        }

        public void Display()
        {
            _displayer.Reset();
            NextTechnique.Display(_displayer);
            _displayer.Show();
        }

        public void Execute()
        {
            _gridHistoryManager.Save();
            NextTechnique.Execute(_executor);
            _displayer.Hide();
        }
    }
}

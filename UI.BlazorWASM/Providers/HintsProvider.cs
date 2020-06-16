using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Hints.SolvingTechniques;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider : IProvider
    {
        private readonly Informer _informer;
        private readonly Displayer _displayer;
        private readonly Executor _executor;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly SudokuProvider _sudokuProvider;
        private readonly HodokuParser _parser = new HodokuParser();


        public event Action OnChanged;

        public HintsState State { get; private set; }
        public void SetState(HintsState value)
        {
            State = value;
            OnChanged?.Invoke();
        }

        private IEnumerable<ISolvingTechnique> Techniques
        {
            get
            {
                yield return new FindIncorrectSolution(_informer);
                yield return new FillMissingCandidates();
                foreach( var technique in _parser.GetSolvingTechniques(_sudokuProvider.Steps).ToList() )
                {
                    yield return technique;
                }
                yield return new NotFound();
            }
        }

        public bool HasExplanation { get; }
        public bool HasNextExplanation { get; }
        public bool HasPreviousExplanation { get; }

        private ISolvingTechnique NextTechnique => Techniques.First(t => t.CanExecute(_informer));

        public HintsProvider(SudokuProvider sudokuProvider, Informer informer, Displayer displayer, Executor executor, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _informer = informer;
            _displayer = displayer;
            _executor = executor;
            _gridHistoryManager = gridHistoryManager;
        }

        private void Display()
        {
            _displayer.Clear();
            NextTechnique.DisplaySolution(_displayer, _informer);
            _displayer.Show();
        }

        public void ShowHint()
        {
            _displayer.Clear();
            NextTechnique.DisplayHint(_displayer, _informer);
            _displayer.Show();
            SetState(Enums.HintsState.ShowHint);
        }

        public void ShowNextStep()
        {
            Display();
            SetState(Enums.HintsState.ShowNextStep);
        }

        public void ShowExplanation()
        {
            SetState(Enums.HintsState.ShowExplanation);
        }

        public void Execute()
        {
            _gridHistoryManager.Save();
            NextTechnique.Execute(_executor, _informer);
            _displayer.Hide();
            SetState(Enums.HintsState.ShowEmpty);
        }


        public void Close()
        {
            Display();
            _displayer.Hide();
            SetState(Enums.HintsState.Hide);
        }

    }
}

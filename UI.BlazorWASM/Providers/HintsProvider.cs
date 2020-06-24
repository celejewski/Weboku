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

        public bool HasExplanation => _currentTechnique.HasExplanation;
        public bool HasNextExplanation => _currentTechnique.HasNextExplanation;
        public bool HasPreviousExplanation => _currentTechnique.HasPreviousExplanation;

        private ISolvingTechnique _currentTechnique;
        private ISolvingTechnique NextTechnique => Techniques.First(t => t.CanExecute(_informer));

        public HintsProvider(SudokuProvider sudokuProvider, Informer informer, Displayer displayer, Executor executor, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _informer = informer;
            _displayer = displayer;
            _executor = executor;
            _gridHistoryManager = gridHistoryManager;
        }


        public void ShowHint()
        {
            _displayer.Clear();
            NextTechnique.DisplayHint(_displayer, _informer);
            _displayer.Show();
            SetState(HintsState.ShowHint);
        }

        public void ShowNextStep()
        {
            _currentTechnique = NextTechnique;
            _displayer.Clear();
            _currentTechnique.DisplaySolution(_displayer, _informer);
            _displayer.Show();
            SetState(HintsState.ShowNextStep);
        }

        public void ShowExplanation()
        {
            _currentTechnique.DisplayExplanation(_displayer, _informer);
            SetState(HintsState.ShowExplanation);
        }

        public void ShowNextExplanation()
        {
            _currentTechnique.NextExplanation();
            ShowExplanation();
        }

        public void ShowPreviousExplanation()
        {
            _currentTechnique.PreviousExplanation();
            ShowExplanation();
        }

        public void Execute()
        {
            _gridHistoryManager.Save();
            NextTechnique.Execute(_executor, _informer);
            _displayer.Hide();
            SetState(HintsState.ShowEmpty);
        }

        public void Close()
        {
            _displayer.Clear();
            _displayer.Hide();
            SetState(HintsState.Hide);
        }

    }
}

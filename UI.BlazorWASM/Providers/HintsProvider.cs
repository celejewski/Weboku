using SmartSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Hints.SolvingTechniqueDisplayers;
using UI.BlazorWASM.Managers;
using SmartSolver.SolvingTechniques;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider : IProvider
    {
        private readonly Informer _informer;
        private readonly Displayer _displayer;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGridProvider _gridProvider;

        public event Action OnChanged;

        public HintsState State { get; private set; }
        public void SetState(HintsState value)
        {
            State = value;
            OnChanged?.Invoke();
        }

        private Solver _solver = new Solver(new SolvingTechniqueFactory());
        private IEnumerable<IDisplaySolvingTechnique> Techniques
        {
            get
            {
                var technique = _solver.NextStep(_gridProvider.Grid) ;
                yield return DisplayTechniqueFactory.GetDisplayer(technique);
            }
        }

        public bool HasExplanation => _currentTechnique.HasExplanation;
        public bool HasNextExplanation => _currentTechnique.HasNextExplanation;
        public bool HasPreviousExplanation => _currentTechnique.HasPreviousExplanation;

        private IDisplaySolvingTechnique _currentTechnique;
        private IDisplaySolvingTechnique NextTechnique => Techniques.First(t => t.CanExecute(_gridProvider.Grid));

        public HintsProvider(
            Informer informer, 
            Displayer displayer,
            IGridHistoryManager gridHistoryManager,
            IGridProvider gridProvider)
        {
            _informer = informer;
            _displayer = displayer;
            _gridHistoryManager = gridHistoryManager;
            _gridProvider = gridProvider;
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
            NextTechnique.Execute(_gridProvider.Grid);
            _gridProvider.Grid = _gridProvider.Grid;
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

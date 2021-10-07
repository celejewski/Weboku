using System;
using Weboku.Application.Hints;
using Weboku.Application.Hints.SolvingTechniqueDisplayers;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public event Action OnHintsChanged;

        public HintsState State { get; private set; }

        public void SetState(HintsState value)
        {
            State = value;
            OnHintsChanged?.Invoke();
        }

        public bool HasExplanation => _currentTechnique.HasExplanation;
        public bool HasNextExplanation => _currentTechnique.HasNextExplanation;
        public bool HasPreviousExplanation => _currentTechnique.HasPreviousExplanation;

        private ISolvingTechniqueDisplayer _currentTechnique;

        private ISolvingTechniqueDisplayer GetNextTechnique()
        {
            var solvingTechnique = GetNextHint();
            var solvingTechniqueDisplayer = DisplayTechniqueFactory.MakeDisplayer(this, solvingTechnique);
            return solvingTechniqueDisplayer;
        }

        public void ShowHint()
        {
            Clear();
            var solvingTechniqueDisplayer = GetNextTechnique();
            solvingTechniqueDisplayer.DisplayHint();
            Show();
            SetState(HintsState.ShowHint);
        }

        public void ShowNextStep()
        {
            _currentTechnique = GetNextTechnique();
            Clear();
            _currentTechnique.DisplaySolution();
            Show();
            SetState(HintsState.ShowNextStep);
        }

        public void ShowExplanation()
        {
            _currentTechnique.DisplayExplanation();
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
            ExecuteNextHint();
            Hide();
            SetState(HintsState.ShowEmpty);
        }

        public void Close()
        {
            Clear();
            Hide();
            SetState(HintsState.Hide);
        }

        public void ShowHintModal()
        {
            SetModalState(Application.Enums.ModalState.Hints);
            SetState(HintsState.ShowEmpty);
        }
    }
}
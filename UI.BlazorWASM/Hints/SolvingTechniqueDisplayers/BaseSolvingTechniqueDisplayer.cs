using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public abstract class BaseSolvingTechniqueDisplayer : IDisplaySolvingTechnique
    {
        protected BaseSolvingTechniqueDisplayer(string locKey)
        {
            _locKey = locKey;
        }

        protected BaseSolvingTechniqueDisplayer(ISolvingTechnique solvingTechnique, string locKey)
        {
            _solvingTechnique = solvingTechnique;
            _locKey = locKey;
        }

        private readonly ISolvingTechnique _solvingTechnique;
        protected string _locKey;
        protected string TitleKey => $"{_locKey}__title";
        protected string DescriptionKey => $"{_locKey}__description";
        protected string ExplanationKey(object index) => $"{_locKey}__explanation-{index}";
        public virtual void DisplayHint(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(TitleKey);
        }

        public virtual void DisplaySolution(Displayer displayer, Informer informer)
        {
            displayer.SetTitle(TitleKey);
            displayer.SetDescription(DescriptionKey);
        }
        public void Execute(IGrid grid)
        {
            _solvingTechnique?.Execute(grid);
        }
        public bool CanExecute(IGrid grid)
        {
            return _solvingTechnique == null || _solvingTechnique.CanExecute(grid);
        }

        protected readonly List<Action<Displayer, Informer>> _explanationSteps = new List<Action<Displayer, Informer>>();

        public bool HasExplanation => _explanationSteps.Count > 0;

        private int _index;
        public bool HasNextExplanation => _explanationSteps.Count - 1 > _index;
        public bool HasPreviousExplanation => _index > 0;
        public void NextExplanation() => _index += 1;
        public void PreviousExplanation() => _index -= 1;
        public void DisplayExplanation(Displayer displayer, Informer informer)
        {
            displayer.Clear();
            _explanationSteps[_index](displayer, informer);
        }

    }
}

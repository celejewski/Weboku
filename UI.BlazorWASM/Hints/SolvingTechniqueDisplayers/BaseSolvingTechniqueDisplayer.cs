using System;
using System.Collections.Generic;
using Core.Data;
using Core.Hints.SolvingTechniques;

namespace Weboku.UserInterface.Hints.SolvingTechniqueDisplayers
{
    public abstract class BaseSolvingTechniqueDisplayer : ISolvingTechniqueDisplayer
    {
        protected BaseSolvingTechniqueDisplayer(Informer informer, Displayer displayer, string locKey)
        {
            _informer = informer;
            _displayer = displayer;
            _locKey = locKey;
        }

        protected BaseSolvingTechniqueDisplayer(Informer informer, Displayer displayer, ISolvingTechnique solvingTechnique, string locKey)
        {
            _informer = informer;
            _displayer = displayer;
            _solvingTechnique = solvingTechnique;
            _locKey = locKey;
        }

        private readonly ISolvingTechnique _solvingTechnique;
        protected readonly Informer _informer;
        protected readonly Displayer _displayer;
        protected string _locKey;
        protected string TitleKey => $"{_locKey}__title";
        protected string DescriptionKey => $"{_locKey}__description";
        protected string ExplanationKey(object index) => $"{_locKey}__explanation-{index}";

        public virtual void DisplayHint()
        {
            _displayer.SetTitle(TitleKey);
        }

        public virtual void DisplaySolution()
        {
            _displayer.SetTitle(TitleKey);
            _displayer.SetDescription(DescriptionKey);
        }

        public void Execute(Grid grid)
        {
            _solvingTechnique?.Execute(grid);
        }

        public bool CanExecute(Grid grid)
        {
            return _solvingTechnique == null || _solvingTechnique.CanExecute(grid);
        }

        protected readonly List<Action> _explanationSteps = new List<Action>();

        public bool HasExplanation => _explanationSteps.Count > 0;

        private int _index;
        public bool HasNextExplanation => _explanationSteps.Count - 1 > _index;
        public bool HasPreviousExplanation => _index > 0;
        public void NextExplanation() => _index += 1;
        public void PreviousExplanation() => _index -= 1;

        public void DisplayExplanation()
        {
            _displayer.Clear();
            _explanationSteps[_index]();
        }
    }
}
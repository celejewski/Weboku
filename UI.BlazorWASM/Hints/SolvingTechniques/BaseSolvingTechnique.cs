using System;
using System.Collections.Generic;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public abstract class BaseSolvingTechnique : ISolvingTechnique
    {
        protected BaseSolvingTechnique(string locKey)
        {
            _locKey = locKey;
        }

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
        }
        public abstract void Execute(Executor executor, Informer informer);
        public abstract bool CanExecute(Informer informer);

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

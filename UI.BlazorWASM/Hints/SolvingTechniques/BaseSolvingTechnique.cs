using System;
using System.Collections.Generic;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public abstract class BaseSolvingTechnique : ISolvingTechnique
    {
        protected BaseSolvingTechnique(string title)
        {
            _title = title;
        }

        protected string _title;
        public void DisplayHint(Displayer displayer, Informer informer) => displayer.SetTitle(_title);
        public abstract void DisplaySolution(Displayer displayer, Informer informer);
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

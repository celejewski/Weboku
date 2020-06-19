namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        void DisplayHint(Displayer displayer, Informer informer);
        void DisplaySolution(Displayer displayer, Informer informer);
        void Execute(Executor executor, Informer informer);
        bool CanExecute(Informer informer);

        bool HasExplanation { get; }
        void DisplayExplanation(Displayer displayer, Informer informer);

        public bool HasNextExplanation { get; }
        public bool HasPreviousExplanation { get; }
        public void NextExplanation();
        public void PreviousExplanation();
    }
}

using Core.Data;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public interface IDisplaySolvingTechnique
    {
        void DisplayHint(Displayer displayer, Informer informer);
        void DisplaySolution(Displayer displayer, Informer informer);
        void Execute(IGrid grid);
        bool CanExecute(IGrid grid);

        bool HasExplanation { get; }
        void DisplayExplanation(Displayer displayer, Informer informer);

        public bool HasNextExplanation { get; }
        public bool HasPreviousExplanation { get; }
        public void NextExplanation();
        public void PreviousExplanation();
    }
}

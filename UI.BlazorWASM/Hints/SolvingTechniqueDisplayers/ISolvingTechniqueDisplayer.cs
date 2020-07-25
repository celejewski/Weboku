using Core.Data;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public interface ISolvingTechniqueDisplayer
    {
        void DisplayHint();
        void DisplaySolution();
        void DisplayExplanation();

        bool CanExecute(IGrid grid);
        void Execute(IGrid grid);

        bool HasExplanation { get; }
        public bool HasNextExplanation { get; }
        public bool HasPreviousExplanation { get; }
        public void NextExplanation();
        public void PreviousExplanation();
    }
}

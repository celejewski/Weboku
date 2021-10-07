using Weboku.Core.Data;

namespace Weboku.Application.Hints.SolvingTechniqueDisplayers
{
    public interface ISolvingTechniqueDisplayer
    {
        void DisplayHint();
        void DisplaySolution();
        void DisplayExplanation();

        bool CanExecute(Grid grid);
        void Execute(Grid grid);

        bool HasExplanation { get; }
        public bool HasNextExplanation { get; }
        public bool HasPreviousExplanation { get; }
        public void NextExplanation();
        public void PreviousExplanation();
    }
}
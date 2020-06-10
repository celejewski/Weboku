using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        string Name { get; }
        string Desc { get; }
        void Display(HintsProvider hintsProvider);
        void Execute(ISudokuProvider sudokuProvider);
        bool CanExecute(ISudokuProvider sudokuProvider);
    }
}

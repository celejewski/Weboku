using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        string Name { get; }
        string Desc { get; }
        void Display();
        void Execute();
        bool CanExecute();
    }
}

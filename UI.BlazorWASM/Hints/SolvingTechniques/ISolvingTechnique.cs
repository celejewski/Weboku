using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        void Display(Displayer displayer, Informer informer);
        void Execute(Executor executor);
        bool CanExecute(Informer informer);
    }
}

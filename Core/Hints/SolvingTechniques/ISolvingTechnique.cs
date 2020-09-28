using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        bool CanExecute(Grid grid);
        void Execute(Grid grid);
    }
}

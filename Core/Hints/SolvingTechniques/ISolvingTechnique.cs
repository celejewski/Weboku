using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        bool CanExecute(IGrid grid);
        void Execute(IGrid grid);
    }
}

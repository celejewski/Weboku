using Core.Data;

namespace SmartSolver.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        bool CanExecute(IGrid grid);
        void Execute(IGrid grid);
    }
}

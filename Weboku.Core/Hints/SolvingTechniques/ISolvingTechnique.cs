using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public interface ISolvingTechnique
    {
        bool CanExecute(Grid grid);
        void Execute(Grid grid);
    }
}
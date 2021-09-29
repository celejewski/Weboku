using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class HintNotFound : ISolvingTechnique
    {
        public bool CanExecute(Grid grid)
        {
            return true;
        }

        public void Execute(Grid grid)
        {
        }
    }
}
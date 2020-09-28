using Core.Data;

namespace Core.Hints.SolvingTechniques
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

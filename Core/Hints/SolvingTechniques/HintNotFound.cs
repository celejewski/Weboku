using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public class HintNotFound : ISolvingTechnique
    {
        public bool CanExecute(IGrid grid)
        {
            return true;
        }

        public void Execute(IGrid grid)
        {
        }
    }
}

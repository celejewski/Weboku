using Core.Data;
using Core.Solvers;

namespace Core.Hints.SolvingTechniques
{
    public class NoSolution : ISolvingTechnique
    {
        private readonly ISolver _solver = new BruteForceSolver();

        public bool CanExecute(IGrid grid)
        {
            return _solver.Solve(grid) == null;
        }

        public void Execute(IGrid grid)
        {
        }
    }
}

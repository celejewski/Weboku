using Core.Data;
using Core.Solvers;

namespace Core.Hints.SolvingTechniques
{
    public class NoSolution : ISolvingTechnique
    {
        private readonly ISolver _solver = new BruteForceSolver();

        public bool CanExecute(Grid grid)
        {
            return _solver.Solve(grid) == null;
        }

        public void Execute(Grid grid)
        {
        }
    }
}

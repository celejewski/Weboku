using Core.Data;
using Core.Solvers;

namespace SmartSolver.SolvingTechniques
{
    public class NoSolution : ISolvingTechnique
    {
        ISolver _solver = new BruteForceSolver();

        public NoSolution()
        {

        }

        public bool CanExecute(IGrid grid)
        {
            return _solver.Solve(grid) == null;
        }

        public void Execute(IGrid grid)
        {
        }
    }
}

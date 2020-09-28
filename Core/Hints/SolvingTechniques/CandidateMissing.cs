using Core.Data;
using Core.Solvers;
using System.Linq;

namespace Core.Hints.SolvingTechniques
{
    public class CandidateMissing : ISolvingTechnique
    {
        private readonly BruteForceSolver _solver = new BruteForceSolver();

        public bool CanExecute(Grid grid)
        {
            var solution = _solver.Solve(grid) ?? _solver.SolveGivens(grid);

            if( solution == null ) return false;

            return Position.Positions.Any(pos => !grid.HasValue(pos)
            && !grid.HasCandidate(pos, solution.GetValue(pos)));
        }

        public void Execute(Grid grid)
        {
            grid.FillAllLegalCandidates();
        }
    }
}

using Core.Data;
using Core.Solvers;
using System;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class CandidateMissing : ISolvingTechnique
    {
        private readonly BruteForceSolver _solver = new BruteForceSolver();

        public bool CanExecute(IGrid grid)
        {
            var solution = _solver.Solve(grid) ?? _solver.SolveGivens(grid);

            if( solution == null ) return false;

            return Position.All.Any(pos => !grid.HasValue(pos) 
            && !grid.HasCandidate(pos, solution.GetValue(pos)));
        }

        public void Execute(IGrid grid)
        {
            grid.FillCandidates();
        }
    }
}

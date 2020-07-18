using Core.Data;
using Core.Solvers;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class InvalidValueFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public InvalidValueFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            BruteForceSolver solver = new BruteForceSolver();
            var solution = solver.Solve(grid) ?? solver.SolveGivens(grid);

            if( solution == null )
            {
                yield return _factory.NoSolution();
                yield break;
            }

            var invalidSolutions = Position.All.Where(pos => grid.HasValue(pos)
                && grid.GetValue(pos) != solution.GetValue(pos));

            if (invalidSolutions.Any())
            {
                yield return _factory.InvalidValue(invalidSolutions);
            }
        }
    }
}

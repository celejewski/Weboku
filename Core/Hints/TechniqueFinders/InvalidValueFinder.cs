using Core.Data;
using Core.Solvers;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class InvalidValueFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            BruteForceSolver solver = new BruteForceSolver();
            var solution = solver.Solve(grid) ?? solver.SolveGivens(grid);

            if( solution == null )
            {
                yield return new NoSolution();
                yield break;
            }

            var invalidSolutions = Position.Positions.Where(pos => grid.HasValue(pos)
                && grid.GetValue(pos) != solution.GetValue(pos));

            if( invalidSolutions.Any() )
            {
                yield return new InvalidValue(invalidSolutions);
            }
        }
    }
}

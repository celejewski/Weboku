﻿using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Solvers;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class InvalidValueFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            BruteForceSolver solver = new BruteForceSolver();
            var solution = solver.Solve(grid) ?? solver.SolveGivens(grid);

            if (solution == null)
            {
                yield return new NoSolution();
                yield break;
            }

            var invalidSolutions = Position.Positions.Where(pos => grid.HasValue(pos)
                                                                   && grid.GetValue(pos) != solution.GetValue(pos));

            if (invalidSolutions.Any())
            {
                yield return new InvalidValue(invalidSolutions);
            }
        }
    }
}
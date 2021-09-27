using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Validators;

namespace Weboku.Core.Solvers
{
    public class BruteForceSolver : BaseSolver
    {
        private static readonly IDictionary<int, Grid> _solved = new Dictionary<int, Grid>();

        private int GetGivensHashcode(Grid grid)
        {
            int hashcode = 0;
            for (int i = 0; i < 81; i++)
            {
                var pos = Position.Positions[i];
                hashcode ^= grid.GetIsGiven(pos)
                    ? grid.GetValue(pos) << (i % 25)
                    : 0;
            }

            return hashcode;
        }

        public override Grid Solve(Grid input)
        {
            ValidatorGrid.EnsureGridIsValid(input);

            var hashcode = GetGivensHashcode(input);
            if (!_solved.ContainsKey(hashcode))
            {
                var grid = input.Clone();
                grid.FillAllLegalCandidates();
                _solved[hashcode] = SolveStep(grid);
            }

            return _solved[hashcode];
        }

        private Grid SolveStep(Grid input)
        {
            if (IsSolved(input))
            {
                return input;
            }

            if (!CanBeSolved(input))
            {
                return null;
            }

            var pos = GetNextPosition(input);
            foreach (var value in Value.NonEmpty.Where(value => input.HasCandidate(pos, value)))
            {
                var grid = input.Clone();
                grid.SetValue(pos, value);

                if (SolveStep(grid) is Grid output)
                {
                    return output;
                }
            }

            return null;
        }

        private bool IsSolved(Grid grid)
        {
            return Position.Positions.All(pos => grid.HasValue(pos));
        }

        private bool CanBeSolved(Grid grid)
        {
            return Position.Positions.All(pos => grid.HasValue(pos) || grid.GetCandidates(pos).Count() > 0);
        }

        private Position GetNextPosition(Grid grid)
        {
            return Position.Positions.Where(pos => !grid.HasValue(pos))
                .Aggregate((nextPos, pos) => grid.GetCandidates(pos).Count() < grid.GetCandidates(nextPos).Count() ? pos : nextPos);
        }
    }
}
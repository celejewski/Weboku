using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Solvers
{
    public class BruteForceSolver : BaseSolver
    {
        static readonly IDictionary<int, IGrid> _solved = new Dictionary<int, IGrid>();

        public override IGrid Solve(IGrid input)
        {
            var hashcode = input.GetGivensHashcode();
            if (!_solved.ContainsKey(hashcode))
            {
                var grid = input.Clone();
                grid.FillCandidates();
                _solved[hashcode] = SolveStep(grid);
            }
            return _solved[hashcode];
        }

        private IGrid SolveStep(IGrid input)
        {
            if( IsSolved(input) )
            {
                return input;
            }

            if( !CanBeSolved(input) )
            {
                return null;
            }

            var pos = GetNextPosition(input);
            foreach( var value in InputValue.NonEmpty.Where(value => input.HasCandidate(pos, value)) )
            {
                var grid = input.Clone();
                grid.SetValue(pos, value);

                if( SolveStep(grid) is Grid output )
                {
                    return output;
                }
            }
            return null;
        }

        private bool IsSolved(IGrid grid)
        {
            return Position.All.All(pos => grid.HasValue(pos));
        }

        private bool CanBeSolved(IGrid grid)
        {
            return Position.All.All(pos => grid.HasValue(pos) || grid.CandidatesCount(pos) > 0);
        }

        private Position GetNextPosition(IGrid grid)
        {
            return Position.All.Where(pos => !grid.HasValue(pos))
                .Aggregate((nextPos, pos) => grid.CandidatesCount(pos) < grid.CandidatesCount(nextPos) ? pos : nextPos);

        }
    }
}

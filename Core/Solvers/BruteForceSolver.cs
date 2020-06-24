using Core.Data;
using System.Linq;

namespace Core.Solvers
{
    public class BruteForceSolver : ISolver
    {
        public IGrid Solve(IGrid input)
        {
            var grid = input.Clone();
            GridHelper.SetAllLegalCandidates(grid);
            return SolveStep(grid);
        }

        private IGrid SolveStep(IGrid input)
        {
            if( IsSolved(input) )
            {
                return input;
            }

            if( GetNextPosition(input) is Position pos)
            {
                foreach( var value in InputValue.NonEmpty.Where(value => input.HasCandidate(pos, value)) )
                {
                    var grid = input.Clone();
                    grid.SetValue(pos, value);
                    GridHelper.RemoveCandidatesSeenBy(grid, pos);

                    if( SolveStep(grid) is Grid output)
                    {
                        return output;
                    }
                }
            }
            return null;
        }

        private bool IsSolved(IGrid grid)
        {
            return Position.All.All(pos => grid.HasValue(pos));
        }

        private Position? GetNextPosition(IGrid grid)
        {
            int count = 10;
            Position? result = null;
            foreach( var pos in Position.All )
            {
                if( grid.GetValue(pos) == InputValue.Empty
                        && grid.GetCandidatesCount(pos) < count )
                {
                    if( grid.GetCandidatesCount(pos) == 0 )
                    {
                        return null;
                    }

                    count = grid.GetCandidatesCount(pos);
                    result = pos;
                }
            }
            return result;
        }
    }
}

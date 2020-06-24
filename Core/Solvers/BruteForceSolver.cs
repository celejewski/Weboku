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

            var pos = GetNextPosition(input);
            if( pos != null )
            {
                for( int value = 1; value < 10; value++ )
                {
                    if( input.HasCandidate(pos.Value, value) )
                    {
                        var grid = input.Clone();
                        grid.SetValue(pos.Value, value);

                        foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos.Value) )
                        {
                            grid.RemoveCandidate(coords, value);
                        }

                        var output = SolveStep(grid);
                        if( output != null )
                        {
                            return output;
                        }
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

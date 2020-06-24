using Core.Data;
using System;

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
                    if( input.HasCandidate(pos.Value.X, pos.Value.Y, (InputValue) value) )
                    {
                        var grid = input.Clone();
                        grid.SetValue(pos.Value.X, pos.Value.Y, (InputValue) value);

                        foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos.Value.X, pos.Value.Y) )
                        {
                            grid.RemoveCandidate(coords.X, coords.Y, (InputValue) value);
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
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    if( grid.GetValue(x, y) == InputValue.Empty )
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Position? GetNextPosition(IGrid grid)
        {
            int count = 10;
            Position? pos = null;
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    if( grid.GetValue(x, y) == InputValue.Empty
                        && grid.GetCandidatesCount(x, y) < count )
                    {
                        if( grid.GetCandidatesCount(x, y) == 0 )
                        {
                            return null;
                        }

                        count = grid.GetCandidatesCount(x, y);
                        pos = new Position(x, y);
                    }
                }
            }
            return pos;
        }
    }
}

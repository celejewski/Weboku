using Core.Data;
using System;

namespace Core.Solver
{
    public class SimpleReverseSolver : ISolver
    {
        public Grid Solve(Grid input)
        {
            var solution = (Grid) input.Clone();

            if( NextStep(solution) )
            {
                return solution;
            }
            else
            {
                throw new ArgumentException("Not solution found");
            }

        }

        private bool NextStep(Grid grid)
        {
            var index = FindLastCell(grid);

            if( !index.HasValue )
            {
                return true;
            }

            var (x, y) = index.Value;

            for( int i = 9 - 1; i >= 0; i-- )
            {
                var value = i + 1;
                if( grid.IsLegalValue(x, y, value) )
                {
                    grid.SetValue(x, y, value);
                    if( NextStep(grid) )
                    {
                        return true;
                    }
                }
            }

            grid.SetValue(x, y, 0);
            return false;
        }

        private (int x, int y)? FindLastCell(Grid grid)
        {
            for( int x = 9 - 1; x >= 0; x-- )
            {
                for( int y = 9 - 1; y >= 0; y-- )
                {
                    if( grid.GetValue(x, y) == 0 )
                    {
                        return (x, y);
                    }
                }
            }
            return null;
        }
    }
}

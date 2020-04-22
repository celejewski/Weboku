using Core.Data;
using System;

namespace Core.Solver
{
    public class SimpleSolver : ISolver
    {
        public Grid Solve(Grid input)
        {
            var solution = (Grid) input.Clone();

            if (NextStep(solution))
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
            var index = FindFirstEmptyCell(grid);

            if (!index.HasValue)
            {
                return true;
            }

            var (x, y) = index.Value;

            for( int i = 0; i < 9; i++ )
            {
                var value = (i + 1).ToString();
                if( grid.IsLegalValue(x, y, value) )
                {
                    grid.SetValue(x, y, value);
                    if( NextStep(grid) )
                    {
                        return true;
                    }
                }
            }

            grid.SetValue(x, y, null);
            return false;
        }

        private (int x, int y)? FindFirstEmptyCell(Grid grid)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    if (grid.GetValue(x, y) == null)
                    {
                        return (x, y);
                    }
                }
            }

            return null;
        }
    }
}

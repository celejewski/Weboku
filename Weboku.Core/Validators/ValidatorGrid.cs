using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Exceptions;

namespace Weboku.Core.Validators
{
    public static class ValidatorGrid
    {
        public static void EnsureGridIsValid(Grid grid)
        {
            if (grid == null)
            {
                throw new InvalidGridException("The grid can not be null.");
            }

            if (!AreAllGivensLegal(grid))
            {
                throw new InvalidGridException("The grid has some invalid givens. Sudoku is unsolvable with this givens.");
            }
        }

        public static bool AreAllGivensLegal(Grid grid)
        {
            return Position.Positions
                .Where(position => grid.GetIsGiven(position))
                .All(position => grid.IsValueLegal(position));
        }

        public static bool AreAllValueslegal(Grid grid)
        {
            return Position.Positions
                .All(position => grid.IsValueLegal(position));
        }
    }
}
using Core.Data;
using Core.Exceptions;
using System.Linq;

namespace Core.Validators
{
    public static class ValidatorGrid
    {
        public static void EnsureGridIsValid(IGrid grid)
        {
            if( grid == null )
            {
                throw new InvalidGridException("The grid can not be null.");
            }

            if( !AreAllInputsLegal(grid) )
            {
                throw new InvalidGridException("The grid has some invalid inputs so grid is unsolvable.");
            }
        }

        public static bool AreAllInputsLegal(IGrid grid)
        {
            return Position.All.All(pos => grid.IsCandidateLegal(pos, grid.GetValue(pos)));
        }
    }
}

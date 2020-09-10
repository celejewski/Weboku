﻿using Core.Data;
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

            if( !AreAllGivensLegal(grid) )
            {
                throw new InvalidGridException("The grid has some invalid givens. Sudoku is unsolvable with this givens.");
            }
        }

        public static bool AreAllGivensLegal(IGrid grid)
        {
            return Position.All
                .Where(pos => grid.GetIsGiven(pos))
                .All(pos => grid.IsCandidateLegal(pos, grid.GetValue(pos)));
        }
    }
}
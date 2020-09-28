﻿using Core.Data;
using System.Linq;

namespace Core.Solvers
{
    public abstract class BaseSolver : ISolver
    {
        public abstract Grid Solve(Grid input);

        public Grid SolveGivens(Grid input)
        {
            var gridWithGivensOnly = input.Clone();
            foreach( var pos in Position.Positions.Where(pos => !gridWithGivensOnly.GetIsGiven(pos)) )
            {
                gridWithGivensOnly.SetValue(pos, Value.None);
            }
            return Solve(gridWithGivensOnly);
        }
    }
}

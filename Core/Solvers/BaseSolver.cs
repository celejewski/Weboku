using Core.Data;
using System.Linq;

namespace Core.Solvers
{
    public abstract class BaseSolver : ISolver
    {
        public abstract IGrid Solve(IGrid input);

        public IGrid SolveGivens(IGrid input)
        {
            var gridWithGivensOnly = input.Clone();
            foreach( var pos in Position.All.Where(pos => !gridWithGivensOnly.GetIsGiven(pos)) )
            {
                gridWithGivensOnly.SetValue(pos, InputValue.None);
            }
            return Solve(gridWithGivensOnly);
        }
    }
}

using Core.Data;

namespace Core.Solvers
{
    public interface ISolver
    {
        IGrid Solve(IGrid input);
    }
}
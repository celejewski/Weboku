using Core.Data;

namespace Core.Solvers
{
    public interface ISolver
    {
        Grid Solve(Grid input);
        Grid SolveGivens(Grid input);
    }
}
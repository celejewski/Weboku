using Weboku.Core.Data;

namespace Weboku.Core.Solvers
{
    public interface ISolver
    {
        Grid Solve(Grid input);
        Grid SolveGivens(Grid input);
    }
}
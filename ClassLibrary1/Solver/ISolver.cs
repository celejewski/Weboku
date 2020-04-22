using Core.Data;

namespace Core.Solver
{
    public interface ISolver
    {
        Grid Solve(Grid input);
    }
}

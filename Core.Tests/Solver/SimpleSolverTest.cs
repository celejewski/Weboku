using Core.Data;
using Core.Solver;
using Xunit;

namespace Core.Tests.Solver
{
    public class SimpleSolverTest : BaseSolverTest
    {
        public SimpleSolverTest()
        {
            _solver = new SimpleSolver();
        }
    }
}

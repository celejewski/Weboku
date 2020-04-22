using Core.Data;
using Core.Solver;
using Xunit;

namespace Core.Tests.Solver
{
    public class SimpleReverseSolverTest : BaseSolverTest
    {
        public SimpleReverseSolverTest()
        {
            _solver = new SimpleReverseSolver();
        }
    }
}

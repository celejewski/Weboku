using Core.Data;
using Core.Solver;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Core.Tests.Solver
{
    public abstract class BaseSolverTest 
    {
        protected ISolver _solver;

        [Fact]
        public void Solve_FindsSolution()
        {
            var input = "000000010400000000020000000000050407008000300001090000300400200050100000000806000";
            var actual = _solver.Solve(new Grid(input)).ToString();

            var expected = "693784512487512936125963874932651487568247391741398625319475268856129743274836159";
            Assert.Equal(expected, actual);

        }
    }
}

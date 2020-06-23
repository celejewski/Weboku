using Core.Converters;
using Core.Generators;
using Core.Solvers;
using System.Runtime.InteropServices;
using Xunit;

namespace Core.Tests
{
    public class SmartSolverTest
    {
        private readonly IGridConverter _gridConverter = new HodokuGridConverter(new EmptyGridGenerator());
        private readonly ISolver _solver = new SmartSolver();

        public string Helper(string input)
        {
            var inputGrid = _gridConverter.FromText(input);
            var outputGrid = _solver.Solve(inputGrid);

            return _gridConverter.ToText(outputGrid);
        }

        [Fact]
        public void FullHouse()
        {
            string input = "597841263643529718.823764593..7.4.82.762851348249135.6238457691451698327.69132845";
            string expected = "597841263643529718182376459315764982976285134824913576238457691451698327769132845";
            var actual = Helper(input);
            Assert.Equal(expected, actual);
        }
    }
}

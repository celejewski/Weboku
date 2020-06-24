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

        [Fact]
        public void NakedSingle()
        {
            string input = "3796854215241379688162497536953721842819645374375..6927534...1916879..45942851376";
            string expected = "379685421524137968816249753695372184281964537437518692753426819168793245942851376";
            var actual = Helper(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Singles()
        {
            string input = "86.7..9....7.3..8.2....4..1385.....................5276..4....3.2..7.6....8..1.75";
            string expected = "864715932157239486293864751385927164712546398946183527679452813521378649438691275";
            var actual = Helper(input);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Singles_2()
        {
            string input = "123.67....4..8....5.............3.563.56.91.229.1.............8....3..9....27.635";
            string expected = "123567849947382561568914327814723956375649182296158473632495718751836294489271635";
            var actual = Helper(input);
            Assert.Equal(expected, actual);
        }
    }
}

using Core.Converters;
using Core.Generators;
using Core.Solvers;
using Xunit;

namespace Core.Tests
{

    public class SolverTest 
    {
        BruteForceSolver _bruteForceSolver = new Solvers.BruteForceSolver();
        IGridConverter _converter = new Converters.HodokuGridConverter(new EmptyGridGenerator());

        [Fact]
        public void Solution1()
        {
            var givens = "..15.6.9.37.19....9.......6...643.1...........4.981...5.......2....25.68.8.4.93..";
            var expected = "821536794376194285954872136295643817138257649647981523569318472413725968782469351";

            var input = _converter.FromText(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.ToText(actual);

            Assert.Equal(expected, actualString);
        }

        public void Solution2()
        {
            var givens = "6.481.9..7..3......3..96....4......1..2...3..9......5....67..8......5..6..3.827.4";
            var expected = "654817923729354618138296475345729861872561349916438257491673582287945136563182794";

            var input = _converter.FromText(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.ToText(actual);

            Assert.Equal(expected, actualString);
        }

        [Fact]
        public void Solution3()
        {
            var givens = "3..5.9...4....8.39..6..31....7.12...5.......4...64.9....98..5..83.2....6...7.4..8";
            var expected = "318569472452178639976423185647912853591387264283645917729836541834251796165794328";

            var input = _converter.FromText(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.ToText(actual);

            Assert.Equal(expected, actualString);
        }
    }
}

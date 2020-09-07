using Core.Exceptions;
using Core.Serializers;
using Core.Solvers;
using Xunit;

namespace Core.Tests
{

    public class SolverTest
    {
        private readonly BruteForceSolver _bruteForceSolver = new BruteForceSolver();
        private readonly IGridSerializer _converter = GridSerializerFactory.Make(GridSerializerName.Hodoku, GridSerializerMode.Everything);

        [Fact]
        public void Solution1()
        {
            const string givens = "..15.6.9.37.19....9.......6...643.1...........4.981...5.......2....25.68.8.4.93..";
            const string expected = "821536794376194285954872136295643817138257649647981523569318472413725968782469351";

            var input = _converter.Deserialize(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.Serialize(actual);

            Assert.Equal(expected, actualString);
        }

        [Fact]
        public void Solution2()
        {
            const string givens = "6.481.9..7..3......3..96....4......1..2...3..9......5....67..8......5..6..3.827.4";
            const string expected = "654817923729354618138296475345729861872561349916438257491673582287945136563182794";

            var input = _converter.Deserialize(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.Serialize(actual);

            Assert.Equal(expected, actualString);
        }

        [Fact]
        public void Solution3()
        {
            const string givens = "3..5.9...4....8.39..6..31....7.12...5.......4...64.9....98..5..83.2....6...7.4..8";
            const string expected = "318569472452178639976423185647912853591387264283645917729836541834251796165794328";

            var input = _converter.Deserialize(givens);
            var actual = _bruteForceSolver.Solve(input);
            var actualString = _converter.Serialize(actual);

            Assert.Equal(expected, actualString);
        }

        [Theory]
        [InlineData(
            "5178....9.639......8.1..53.6.1.5.................1.9.4.42..6.9......321.7....1486",
            "517832649263945871984167532691354728428679153375218964142786395856493217739521486")]
        [InlineData(
            ".......4.....24..8...5.96...6....1.91..283..72.5....8...43.1...8..75.....5.......",
            "512867943936124758487539612368475129149283567275916384624391875891752436753648291")]
        [InlineData(
            "3....25687..........8.6.9..15.47.......3.5.......16.54..2.8.1..........74192....5",
            "391742568765198432248563971153479826684325719927816354572684193836951247419237685")]
        [InlineData(
            ".2.........8..65..4.....79.5..4.392.2..6.1..5.795.2..1.56.....3..73..6.........8.",
            "125749368798236514463158792581473926234691875679582431956817243817324659342965187")]
        [InlineData(
            "5..8..2......2.71..8..764..3..7.4...............9.3..6..845..9..51.9......9..2..5",
            "597841263643529718182376459315764982976285134824913576238457691451698327769132845")]
        public void MultipleSudokus(string input, string expected)
        {
            var grid = _converter.Deserialize(input);
            var actual = _bruteForceSolver.Solve(grid);
            var actualString = _converter.Serialize(actual);

            Assert.Equal(expected, actualString);
        }

        [Fact]
        public void NoSolution()
        {
            const string givens = ".....9...571..5....9.....83...49.5..82.6.3.74..9.28...63.....1....8..43....9.....";
            var grid = _converter.Deserialize(givens);
            Assert.Throws(typeof(InvalidGridException), () => _bruteForceSolver.Solve(grid));
        }
    }
}

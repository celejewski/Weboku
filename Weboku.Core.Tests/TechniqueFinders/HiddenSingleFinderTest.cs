using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Hints.TechniqueFinders;
using Weboku.Core.Serializers;
using Xunit;

namespace Weboku.Core.Tests.TechniqueFinders
{
    public class HiddenSingleFinderTest
    {
        private readonly HiddenSingleFinder _finder = new HiddenSingleFinder();

        [Fact]
        public void FindsOne()
        {
            const string givens = "2..364975369..2184......263........79.723.5466.......1793.2.418846193752125487639";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var results = _finder.FindAll(grid).OfType<HiddenSingle>().ToList();
            Assert.Equal(2, results.Count);

            var result = results[0];
            Assert.Equal(Value.Seven, result.Value);
            Assert.True(result.House == House.Block || result.House == House.Col);
            Assert.True((1, 2) == (result.Position.X, result.Position.Y));
        }

        [Fact]
        public void NoSolution()
        {
            const string givens = "9.....2.....2.761917269...429.5763..6.384279.7.8931.628..729.53529........7...92.";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var results = _finder.FindAll(grid);

            Assert.Empty(results);
        }

        [Fact]
        public void NoExceptions()
        {
            var givens = new string[]
            {
                ".....58...4.........7428.6.71..64..5....1....9..38..76.2.1536.........4...52.....",
                "............8.1247..426.1...619.....9.7...5.2.....596...2.978..7356.2............",
                "52...8....6..235....15.......5.1..797...8...534..9.1.......28....285..9....4...23",
                "..487..5......3..639......8......53...39526...69......8......459..7......2..381..",
                "62...47......1.....456...2.8.....4...6.9.3.5...1.....7.7...184.....9......65...71",
            };

            foreach (var given in givens)
            {
                var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(given);
                grid.FillAllLegalCandidates();

                _finder.FindAll(grid).ToList();
            }
        }
    }
}
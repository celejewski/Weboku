using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Hints.TechniqueFinders;
using Weboku.Core.Serializers;
using Xunit;

namespace Weboku.Core.Tests.TechniqueFinders
{
    public class NakedSingleFinderTest
    {
        [Fact]
        public void FindsOne()
        {
            const string givens = ".7.......8..745...5.2.893.........712.7...6.445.........542.1.3...651..8.......2.";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            (int x, int y) expectedPositions = (3, 2);
            Value expectedValue = 1;

            var finder = new NakedSingleFinder();
            var results = finder.FindAll(grid).OfType<NakedSingle>();

            Assert.Single(results);
            var result = results.Single();
            Assert.Equal(expectedValue, result.Value);
            Assert.True(expectedPositions == (result.Position.x, result.Position.y));
        }
    }
}
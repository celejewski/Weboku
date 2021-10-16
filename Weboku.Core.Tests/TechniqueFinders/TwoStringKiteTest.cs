using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Hints.TechniqueFinders;
using Weboku.Core.Serializers;
using Xunit;

namespace Weboku.Core.Tests.TechniqueFinders
{
    public class TwoStringKiteTest
    {
        private readonly ITechniqueFinder _finder = new TwoStringKiteFinder();

        [Fact]
        public void FindSingle()
        {
            const string givens = "EIRgDqW22GMovaU8am095lFCgjGUAQqGGCBYgQLEysDUGwEzEuCABYbUAQYKdKQEICYg1thbbghGIAgTiGoJQMq1BSDFNAA";
            var grid = GridSerializerFactory.Make(GridSerializerName.Base64).Deserialize(givens);

            var results = _finder.FindAll(grid).OfType<TwoStringKite>().ToList();
            Assert.Single(results);
            var result = results[0];
            Assert.Equal(Value.Five, result.Value);

            var expectedLegalPositions = new (int x, int y)[] {(5, 5), (1, 6)};
            var expectedInfoPositions = new (int x, int y)[] {(3, 6), (5, 8)};
            (int x, int y) expectedPositionToRemove = (1, 5);
            Assert.All(result.LegalPositions,
                pos1 => expectedLegalPositions.Any(pos2 => (pos1.X, pos1.Y) == (pos2.x, pos2.y)));
            Assert.All(result.InfoPositions,
                pos1 => expectedInfoPositions.Any(pos2 => (pos1.X, pos1.Y) == (pos2.x, pos2.y)));
            Assert.Equal((result.PositionToRemove.X, result.PositionToRemove.Y), (expectedPositionToRemove.x, expectedPositionToRemove.y));
        }
    }
}
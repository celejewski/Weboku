using Core.Data;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using System.Linq;
using Xunit;

namespace Tests.TechniqueFinders
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
            Assert.Equal(InputValue.Five, result.Value);

            var expectedLegalPositions = new (int x, int y)[] { (5, 5), (1, 6) };
            var expectedInfoPositions = new (int x, int y)[] { (3, 6), (5, 8) };
            (int x, int y) expectedPositionToRemove = (1, 5);
            Assert.All(result.LegalPositions,
                pos1 => expectedLegalPositions.Any(pos2 => (pos1.x, pos1.y) == (pos2.x, pos2.y)));
            Assert.All(result.InfoPositions,
                pos1 => expectedInfoPositions.Any(pos2 => (pos1.x, pos1.y) == (pos2.x, pos2.y)));
            Assert.Equal((result.PositionToRemove.x, result.PositionToRemove.y), (expectedPositionToRemove.x, expectedPositionToRemove.y));
        }
    }
}

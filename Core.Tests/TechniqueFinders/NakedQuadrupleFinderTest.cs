using System.Linq;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using Xunit;

namespace Core.Tests.TechniqueFinders
{
    public class NakedQuadrupleFinderTest
    {
        [Fact]
        public void FindsOne()
        {
            const string givens = ".9...283.....45.1.........7..723...1....6....4...876..8.........1.92.....597...8.";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var finder = new NakedQuadrupleFinder();
            var results = finder.FindAll(grid).OfType<NakedSubset>().ToList();

            Assert.Single(results);

            var result = results[0];

            var expectedValues = new[] {4, 5, 6, 9};
            var expectedPositions = new (int x, int y)[]
            {
                (3, 0),
                (3, 5),
                (3, 6),
                (3, 7),
            };

            Assert.All(result.Values, value => expectedValues.Contains(value));
            Assert.All(expectedValues, value => result.Values.Contains(value));

            Assert.All(expectedPositions, expectedPos => result.Positions
                .Any(actualPos => (expectedPos.x, expectedPos.y) == (actualPos.x, actualPos.y)));
            Assert.All(result.Positions, actualPos => expectedPositions
                .Any(expectedPos => (actualPos.x, actualPos.y) == (expectedPos.x, expectedPos.y)));
        }
    }
}
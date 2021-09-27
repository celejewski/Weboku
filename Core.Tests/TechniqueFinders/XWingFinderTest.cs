using System.Linq;
using Core.Data;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using Xunit;

namespace Core.Tests.TechniqueFinders
{
    public class XWingFinderTest
    {
        [Fact]
        public void FindOne()
        {
            const string givens = "1.....5973...2.....4.9...1....58......83.26......14....5...9.8.....5...4739.....2";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var finder = new XWingFinder();

            var result = finder.FindAll(grid).OfType<XWing>().ToList();

            Assert.Single(result);

            var first = result[0];
            Assert.Equal(Value.Four, first.Value);
            Assert.Equal(House.Row, first.House);

            var points = new[]
            {
                (3, 0),
                (4, 0),
                (3, 8),
                (4, 8),
            };

            Assert.True(points.All(point => first.Positions.Any(pos => (pos.x, pos.y) == point)));
        }
    }
}
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;
using Weboku.Core.Hints.TechniqueFinders;
using Weboku.Core.Serializers;
using Xunit;

namespace Weboku.Core.Tests.TechniqueFinders
{
    public class FullHouseFinderTest
    {
        private readonly ITechniqueFinder _finder = new FullHouseFinder();

        [Fact]
        public void FindCol()
        {
            const string givens = "..4156728215873496678249513....21367.61387.4...769.18.1.3.628747...3.651.4671.239";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);

            var results = _finder.FindAll(grid).OfType<FullHouse>().ToList();
            Assert.Single(results);
            var result = results[0];
            Assert.Equal(Value.Nine, result.Value);
            Assert.True((6, 4) == (result.Position.X, result.Position.Y));
        }

        [Fact]
        public void FindBlock()
        {
            const string givens = "816..9247537..216.2..17683546972.5.332.9.467.7.1.63924.736954.26..2.739...2...756";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);

            var results = _finder.FindAll(grid).OfType<FullHouse>().ToList();
            Assert.Single(results);
            var result = results[0];
            Assert.Equal(Value.Nine, result.Value);
            Assert.True(result.Position.Equals((8, 1)));
        }
    }
}
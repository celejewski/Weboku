using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using System.Linq;
using Xunit;

namespace Tests.TechniqueFinders
{
    public class LockedCandidatesPointingFinderTests
    {
        private readonly ITechniqueFinder _finder
            = new LockedCandidatesPointingFinder();

        [Fact]
        public void FindAll()
        {
            const string givens = "...91...7..1..6394.694..15..94.3.2.....6.1943..3.4978..18.67439..6......9...84...";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var results = _finder.FindAll(grid).OfType<LockedCandidatesPointing>().ToList();

            Assert.Equal(3, results.Count);

            var expectedValues = new[] { 3, 5, 8 };
            Assert.True(expectedValues.All(value => results.Any(t => t.InputValue == value)));
        }
    }
}

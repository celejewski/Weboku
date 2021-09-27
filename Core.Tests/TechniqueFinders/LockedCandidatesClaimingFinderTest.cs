using System.Linq;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using Xunit;

namespace Core.Tests.TechniqueFinders
{
    public class LockedCandidatesClaimingFinderTest
    {
        private readonly ITechniqueFinder _finder
            = new LockedCandidatesClaimingFinder();

        [Fact]
        public void FindAll()
        {
            const string givens = "....3.2.4.....19.6.4.5..37.6...5..23.82...1955......678273.564.4.56..7...69.8.5..";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);
            grid.FillAllLegalCandidates();

            var results = _finder.FindAll(grid).OfType<LockedCandidatesClaiming>().ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(results, result => result.Value == 4);
            Assert.Contains(results, result => result.Value == 9);

            (int r, int c)[] expectedPositions = new[]
            {
                (4, 4),
                (4, 6),
                (6, 4),
                (6, 5),
                (6, 6),
            };

            var result = results.First(result => result.Value == 4);

            foreach (var (r, c) in expectedPositions)
            {
                var x = c - 1;
                var y = r - 1;
                Assert.Contains(result.PositionsToRemoveCandidate, pos => (pos.x, pos.y) == (x, y));
            }
        }
    }
}
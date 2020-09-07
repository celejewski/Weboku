using Core.Data;
using Core.Hints.SolvingTechniques;
using Core.Hints.TechniqueFinders;
using Core.Serializers;
using System.Linq;
using Xunit;

namespace SmartSolver.Tests.TechniqueFinders
{
    public class HiddenSingleWithoutCandidatesFinderTest
    {
        private readonly ITechniqueFinder _finder =
            new HiddenSingleWithoutCandidatesFinder();

        [Fact]
        public void FindsAll()
        {
            const string givens = "3..1....9...4..26..7..298..9....5......812......9....1..469..3..87..3...1....8..2";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);

            var techniques = _finder.FindAll(grid).OfType<HiddenSingle>().ToList();

            (int r, int c, int value)[] expected = new[]{
                (7, 9, 8),
                (8, 4, 2),
                (7, 6, 1),
                (6, 6, 4),
                (3, 8, 1),
            };

            int count = 0;
            foreach( var (row, col, value) in expected )
            {
                var x = col - 1;
                var y = row - 1;
                var found = techniques.Count(technique => technique.Position.x == x && technique.Position.y == y && technique.Value == value);
                Assert.True(found > 0);
                count += found;
            }
            Assert.Equal(count, techniques.Count);
        }

        [Fact]
        public void FindNone()
        {
            const string givens = ".7...9..3185327496..9..1......2.8.....87146.....9.3...853172964916435782427896351";
            var grid = GridSerializerFactory.Make(GridSerializerName.Hodoku).Deserialize(givens);

            var techniques = _finder.FindAll(grid).ToList();

            Assert.False(techniques.Count > 0);
        }
    }
}

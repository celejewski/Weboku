using Core.Data;
using Core.Factories;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System.Linq;
using Xunit;

namespace SmartSolver.Tests
{
    public class HiddenSingleWithoutCandidatesFinderTest
    {
        private ITechniqueFinder _finder =
            new HiddenSingleWithoutCandidatesFinder(new SolvingTechniqueFactory());

        [Fact]
        public void FindsAll()
        {
            var givens = "3..1....9...4..26..7..298..9....5......812......9....1..469..3..87..3...1....8..2";
            var grid = GridFactory.FromText(givens);

            var techniques = _finder.FindAll(grid).OfType<HiddenSingle>().ToList();

            (int r, int c, int value)[] expected = new[]{
                (7, 9, 8),
                (8, 4, 2),
                (7, 6, 1),
                (6, 6, 4),
                (3, 8, 1),
            };

            int count = 0;
            foreach( var item in expected )
            {
                var pos = new Position(item.c - 1, item.r - 1);
                var found = techniques.Count(t => t.Position.Equals(pos) && t.Value == item.value);
                Assert.True(found > 0);
                count += found;
            }
            Assert.Equal(count, techniques.Count);
        }

        [Fact]
        public void FindNone()
        {
            var givens = ".7...9..3185327496..9..1......2.8.....87146.....9.3...853172964916435782427896351";
            var grid = GridFactory.FromText(givens);

            var techniques = _finder.FindAll(grid).ToList();

            Assert.False(techniques.Any());
        }
    }
}

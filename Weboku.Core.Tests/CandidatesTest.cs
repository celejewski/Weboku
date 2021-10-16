using FluentAssertions;
using Weboku.Core.Data;
using Xunit;

namespace Weboku.Core.Tests
{
    public class CandidatesTest
    {
        [Theory]
        [InlineData(Candidates.None, 0)]
        [InlineData(Candidates.One, 1)]
        [InlineData(Candidates.One | Candidates.Two, 2)]
        [InlineData(Candidates.One | Candidates.Three, 2)]
        [InlineData(Candidates.One | Candidates.Nine | Candidates.Five, 3)]
        [InlineData(Candidates.All, 9)]
        public void Candidates_Count_should_depend_on_selected_candidates(Candidates candidates, int expected)
        {
            var actual = candidates.Count();
            actual.Should().Be(expected);
        }

        [Fact]
        public void ToValues_should_return_empty_list_for_none_candidates()
        {
            var candidates = Candidates.None;
            var actual = candidates.ToValues();
            actual.Should().BeEmpty();
        }

        [Fact]
        public void ToValues_should_return_only_selected_candidates()
        {
            var candidates = Candidates.One | Candidates.Five;
            var actual = candidates.ToValues();
            actual.Should().HaveCount(2);
            actual.Should().Contain(Value.One);
            actual.Should().Contain(Value.Five);
        }

        [Fact]
        public void ToValues_should_return_all_values_for_all_candidates()
        {
            var candidates = Candidates.All;
            var actual = candidates.ToValues();

            actual.Should().Contain(Value.One);
            actual.Should().Contain(Value.Two);
            actual.Should().Contain(Value.Three);
            actual.Should().Contain(Value.Four);
            actual.Should().Contain(Value.Five);
            actual.Should().Contain(Value.Six);
            actual.Should().Contain(Value.Seven);
            actual.Should().Contain(Value.Eight);
            actual.Should().Contain(Value.Nine);
            actual.Should().HaveCount(9);
        }
    }
}
using Weboku.Core.Data;
using Xunit;

namespace Weboku.Core.Tests
{
    public class CandidatesTest
    {
        [Fact]
        public void CountIsValid()
        {
            Assert.Equal(0, Candidates.None.Count());
            Assert.Equal(1, Candidates.One.Count());
            Assert.Equal(1, Candidates.Five.Count());
            Assert.Equal(2, (Candidates.One | Candidates.Two).Count());
            Assert.Equal(2, (Candidates.One | Candidates.Five).Count());
            Assert.Equal(2, (Candidates.One | Candidates.Five).Count());
            Assert.Equal(9, (Candidates.All).Count());
        }

        [Fact]
        public void ToReadOnlyListIsValid()
        {
            Assert.Single(Candidates.One.ToValues());
            Assert.Contains(Value.One, Candidates.One.ToValues());

            var oneAndTwo = (Candidates.Two | Candidates.One).ToValues();
            Assert.Equal(2, oneAndTwo.Count);
            Assert.Contains(Value.One, oneAndTwo);
            Assert.Contains(Value.Two, oneAndTwo);
            Assert.DoesNotContain(Value.Three, oneAndTwo);
            Assert.DoesNotContain(Value.Nine, oneAndTwo);
        }
    }
}
using Core.Data;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests
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
            Assert.Single(Candidates.One.ToInputValues());
            Assert.Contains(InputValue.One, Candidates.One.ToInputValues());

            var oneAndTwo = (Candidates.Two | Candidates.One).ToInputValues();
            Assert.Equal(2, oneAndTwo.Count);
            Assert.Contains(InputValue.One, oneAndTwo);
            Assert.Contains(InputValue.Two, oneAndTwo);
            Assert.DoesNotContain(InputValue.Three, oneAndTwo);
            Assert.DoesNotContain(InputValue.Nine, oneAndTwo);
        }
    }
}

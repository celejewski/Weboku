using Core.Data;
using Xunit;
using System;

namespace Core.Tests
{
    public class GridTest
    {
        private readonly IGrid _grid = new Grid();
        private readonly Random _random = new Random();

        [Fact]
        public void CandidatesCount()
        {
            for( int i = 0; i < 1_000_000; i++ )
            {
                var x = _random.Next(9);
                var y = _random.Next(9);

                _grid.ClearCandidates(x, y);
                var repeats = _random.Next(10) + 1;
                for( int j = 0; j < repeats; j++ )
                {
                    var value = (InputValue) (_random.Next(9) + 1);
                    _grid.AddCandidate(x, y, value);
                }
                Assert.InRange(_grid.GetCandidatesCount(x, y), 1, 9);
            }
        }
    }
}

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
                var pos = new Position(x, y);

                _grid.ClearCandidates(pos);
                var repeats = _random.Next(10) + 1;
                for( int j = 0; j < repeats; j++ )
                {
                    var value = (InputValue) (_random.Next(9) + 1);
                    _grid.AddCandidate(pos, value);
                }
                Assert.InRange(_grid.GetCandidatesCount(pos), 1, 9);
            }
        }
    }
}

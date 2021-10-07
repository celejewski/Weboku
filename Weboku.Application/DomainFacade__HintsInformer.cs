using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public IEnumerable<Position> GetPositionsWithCandidate(House house, Position housePosition, Value value)
        {
            return HintsHelper
                .GetPositionsInHouse(housePosition, house)
                .Where(pos => HasCandidate(pos, value));
        }

        public IEnumerable<Position> WithCandidate(IEnumerable<Position> positions, Value value)
        {
            return positions.Where(pos => HasCandidate(pos, value));
        }
    }
}
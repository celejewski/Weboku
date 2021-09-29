using System.Collections.Generic;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class NakedPair : NakedSubset
    {
        public NakedPair(IEnumerable<Position> positions, IEnumerable<Value> values)
            : base(positions, values)
        {
        }
    }
}
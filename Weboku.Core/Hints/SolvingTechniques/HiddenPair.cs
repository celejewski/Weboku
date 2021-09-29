using System.Collections.Generic;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class HiddenPair : HiddenSubset
    {
        public HiddenPair(IEnumerable<Position> positions, IEnumerable<Value> values)
            : base(positions, values)
        {
        }
    }
}
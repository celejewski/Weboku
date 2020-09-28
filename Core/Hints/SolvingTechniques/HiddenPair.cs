using Core.Data;
using System.Collections.Generic;

namespace Core.Hints.SolvingTechniques
{
    public class HiddenPair : HiddenSubset
    {
        public HiddenPair(IEnumerable<Position> positions, IEnumerable<Value> values)
            : base(positions, values)
        {
        }
    }
}

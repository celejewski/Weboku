using Core.Data;
using System.Collections.Generic;

namespace Core.Hints.SolvingTechniques
{
    public class NakedPair : NakedSubset
    {
        public NakedPair(IEnumerable<Position> positions, IEnumerable<Value> values)
            : base(positions, values)
        {
        }
    }
}

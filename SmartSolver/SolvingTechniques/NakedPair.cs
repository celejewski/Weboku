using Core.Data;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public class NakedPair : NakedSubset
    {
        public NakedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            :base(positions, values)
        {

        }
    }
}

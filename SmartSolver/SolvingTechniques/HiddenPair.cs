using Core.Data;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public class HiddenPair : HiddenSubset
    {
        public HiddenPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            : base(positions, values)
        {

        }
    }
}

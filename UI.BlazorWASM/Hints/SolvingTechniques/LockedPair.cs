using Core.Data;
using System.Collections.Generic;
using System.Linq;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public class LockedPair : NakedPair
    {
        public LockedPair(IEnumerable<Position> positions, IEnumerable<InputValue> values)
            : base(positions, values)
        {
            _title = "Locked pair";
        }

    }
}

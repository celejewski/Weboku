using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolver.SolvingTechniques
{
    public class LockedCandidatesPointing : ISolvingTechnique
    {
        public int Block { get; }
        public InputValue InputValue { get; }
        public IEnumerable<Position> PositionsToRemoveFrom { get; }

        public LockedCandidatesPointing(int block, InputValue inputValue, IEnumerable<Position> positionsToRemoveFrom)
        {
            Block = block;
            InputValue = inputValue;
            PositionsToRemoveFrom = positionsToRemoveFrom;
        }

        public bool CanExecute(IGrid grid)
        {
            return PositionsToRemoveFrom.Any(pos => grid.HasCandidate(pos, InputValue));
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in PositionsToRemoveFrom )
            {
                grid.RemoveCandidate(pos, InputValue);
            }
        }
    }
}

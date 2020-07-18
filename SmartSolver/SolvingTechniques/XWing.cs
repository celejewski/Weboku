using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class XWing : ISolvingTechnique
    {
        public XWing(InputValue value, IEnumerable<Position> positions, IEnumerable<Position> positionsToRemove, House house)
        {
            Value = value;
            Positions = positions;
            PositionsToRemove = positionsToRemove;
            House = house;
        }

        public InputValue Value { get; }
        public IEnumerable<Position> Positions { get; }
        public IEnumerable<Position> PositionsToRemove { get; }
        public House House { get; }

        public bool CanExecute(IGrid grid)
        {
            return PositionsToRemove.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in PositionsToRemove )
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}

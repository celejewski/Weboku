using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.SolvingTechniques
{
    public class XWing : ISolvingTechnique
    {
        public XWing(Value value, IEnumerable<Position> positions, IEnumerable<Position> positionsToRemove, House house)
        {
            Value = value;
            Positions = positions;
            PositionsToRemove = positionsToRemove;
            House = house;
        }

        public Value Value { get; }
        public IEnumerable<Position> Positions { get; }
        public IEnumerable<Position> PositionsToRemove { get; }
        public House House { get; }

        public bool CanExecute(Grid grid)
        {
            return PositionsToRemove.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(Grid grid)
        {
            foreach( var pos in PositionsToRemove )
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}

using Core.Data;
using System.Linq;

namespace Core.Hints.SolvingTechniques
{
    public class Skyscrapper : ISolvingTechnique
    {
        public Skyscrapper(Position base1, Position base2, Position pos1, Position pos2, InputValue value)
        {
            Base1 = base1;
            Base2 = base2;
            Pos1 = pos1;
            Pos2 = pos2;
            Value = value;
        }

        public Position Base1 { get; }
        public Position Base2 { get; }
        public Position Pos1 { get; }
        public Position Pos2 { get; }
        public InputValue Value { get; }

        public bool CanExecute(IGrid grid)
        {
            return Position.GetOtherPositionsSeenBy(Pos1, Pos2)
                  .Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in Position.GetOtherPositionsSeenBy(Pos1, Pos2) )
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}

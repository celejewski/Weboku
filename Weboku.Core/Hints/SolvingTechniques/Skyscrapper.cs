using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class Skyscrapper : ISolvingTechnique
    {
        public Skyscrapper(Position base1, Position base2, Position pos1, Position pos2, Value value)
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
        public Value Value { get; }

        public bool CanExecute(Grid grid)
        {
            return Position.GetPositionsSeenByAll(Pos1, Pos2)
                .Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(Grid grid)
        {
            foreach (var pos in Position.GetPositionsSeenByAll(Pos1, Pos2))
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}
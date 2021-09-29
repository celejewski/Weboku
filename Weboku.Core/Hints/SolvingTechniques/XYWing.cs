using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class XYWing : ISolvingTechnique
    {
        public XYWing(Position pivot, Position pos1, Position pos2, Value candidate1, Value candidate2, IEnumerable<Position> positionsToRemove, Value value)
        {
            Pivot = pivot;
            Pos1 = pos1;
            Pos2 = pos2;
            Candidate1 = candidate1;
            Candidate2 = candidate2;
            PositionsToRemove = positionsToRemove;
            Value = value;
        }

        public Position Pivot { get; }
        public Position Pos1 { get; }
        public Position Pos2 { get; }
        public Value Candidate1 { get; }
        public Value Candidate2 { get; }
        public IEnumerable<Position> PositionsToRemove { get; }
        public Value Value { get; }

        public bool CanExecute(Grid grid)
        {
            return PositionsToRemove.Any(pos => grid.HasCandidate(pos, Value));
        }

        public void Execute(Grid grid)
        {
            foreach (var pos in PositionsToRemove)
            {
                grid.RemoveCandidate(pos, Value);
            }
        }
    }
}
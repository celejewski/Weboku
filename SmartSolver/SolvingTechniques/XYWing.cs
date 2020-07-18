using Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.SolvingTechniques
{
    public class XYWing : ISolvingTechnique
    {
        public XYWing(Position pivot, Position pos1, Position pos2, InputValue candidate1, InputValue candidate2, IEnumerable<Position> positionsToRemove, InputValue value)
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
        public InputValue Candidate1 { get; }
        public InputValue Candidate2 { get; }
        public IEnumerable<Position> PositionsToRemove { get; }
        public InputValue Value { get; }

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

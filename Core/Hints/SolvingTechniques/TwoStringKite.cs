using System.Collections.Generic;
using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
{
    public class TwoStringKite : ISolvingTechnique
    {
        public TwoStringKite(Value value, IEnumerable<Position> legalPositions, IEnumerable<Position> infoPositions, Position positionToRemove)
        {
            Value = value;
            LegalPositions = legalPositions;
            InfoPositions = infoPositions;
            PositionToRemove = positionToRemove;
        }

        public Value Value { get; }
        public IEnumerable<Position> LegalPositions { get; }
        public IEnumerable<Position> InfoPositions { get; }
        public Position PositionToRemove { get; }

        public bool CanExecute(Grid grid)
        {
            return grid.HasCandidate(PositionToRemove, Value);
        }

        public void Execute(Grid grid)
        {
            grid.RemoveCandidate(PositionToRemove, Value);
        }
    }
}
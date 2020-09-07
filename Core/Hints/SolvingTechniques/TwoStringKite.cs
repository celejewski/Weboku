using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hints.SolvingTechniques
{
    public class TwoStringKite : ISolvingTechnique
    {
        public TwoStringKite(InputValue value, IEnumerable<Position> legalPositions, IEnumerable<Position> infoPositions, Position positionToRemove )
        {
            Value = value;
            LegalPositions = legalPositions;
            InfoPositions = infoPositions;
            PositionToRemove = positionToRemove;
        }

        public InputValue Value { get; }
        public IEnumerable<Position> LegalPositions { get; }
        public IEnumerable<Position> InfoPositions { get; }
        public Position PositionToRemove { get; }

        public bool CanExecute(IGrid grid)
        {
            return grid.HasCandidate(PositionToRemove, Value);
        }

        public void Execute(IGrid grid)
        {
            grid.RemoveCandidate(PositionToRemove, Value);
        }
    }
}

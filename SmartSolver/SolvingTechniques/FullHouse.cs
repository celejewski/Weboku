using Core.Data;
using System;

namespace SmartSolver.SolvingTechniques
{
    public class FullHouse : ISolvingTechnique
    {
        public FullHouse(Position position, InputValue value)
        {
            Position = position;
            Value = value;
        }

        public InputValue Value { get; }

        public Position Position { get; }

        public bool CanExecute(IGrid grid)
        {
            return grid.HasCandidate(Position, Value);
        }

        public void Execute(IGrid grid)
        {
            grid.SetValue(Position, Value);
        }
    }
}

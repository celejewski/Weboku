using Core.Data;
using System;

namespace SmartSolver.SolvingTechniques
{
    public class HiddenSingle : ISolvingTechnique
    {
        public HiddenSingle(Position position, InputValue value)
        {
            Position = position;
            Value = value;
        }

        public Position Position { get; }
        public InputValue Value { get; }

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

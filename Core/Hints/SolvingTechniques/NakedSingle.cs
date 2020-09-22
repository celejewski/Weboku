using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public class NakedSingle : ISolvingTechnique
    {
        public NakedSingle(Position position, Value value)
        {
            Position = position;
            Value = value;
        }
        public Position Position { get; }
        public Value Value { get; }

        public bool CanExecute(IGrid grid) => grid.HasCandidate(Position, Value);

        public void Execute(IGrid grid) => grid.SetValue(Position, Value);
    }
}

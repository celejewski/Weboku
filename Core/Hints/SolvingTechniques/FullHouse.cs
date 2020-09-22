using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public class FullHouse : ISolvingTechnique
    {
        public FullHouse(Position position, Value value)
        {
            Position = position;
            Value = value;
        }

        public Value Value { get; }

        public Position Position { get; }

        public bool CanExecute(IGrid grid)
        {
            return true;
        }

        public void Execute(IGrid grid)
        {
            grid.SetValue(Position, Value);
        }
    }
}

using Weboku.Core.Data;

namespace Weboku.Core.Hints.SolvingTechniques
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

        public bool CanExecute(Grid grid)
        {
            return true;
        }

        public void Execute(Grid grid)
        {
            grid.SetValue(Position, Value);
        }
    }
}
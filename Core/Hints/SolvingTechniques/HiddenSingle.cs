using Core.Data;

namespace Core.Hints.SolvingTechniques
{
    public class HiddenSingle : ISolvingTechnique
    {
        public HiddenSingle(Position position, InputValue value, House house)
        {
            Position = position;
            Value = value;
            House = house;
        }

        public Position Position { get; }
        public InputValue Value { get; }
        public House House { get; }

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

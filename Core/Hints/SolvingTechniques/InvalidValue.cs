using Core.Data;
using System.Collections.Generic;

namespace Core.Hints.SolvingTechniques
{
    public class InvalidValue : ISolvingTechnique
    {
        private readonly IEnumerable<Position> _positions;

        public InvalidValue(IEnumerable<Position> positions)
        {
            _positions = positions;
        }

        public bool CanExecute(Grid grid)
        {
            return true;
        }

        public void Execute(Grid grid)
        {
            foreach( var pos in _positions )
            {
                grid.SetValue(pos, Value.None);
            }
        }
    }
}

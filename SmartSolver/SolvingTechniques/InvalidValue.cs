using Core.Data;
using System.Collections.Generic;

namespace SmartSolver.SolvingTechniques
{
    public class InvalidValue : ISolvingTechnique
    {
        private readonly IEnumerable<Position> _positions;

        public InvalidValue(IEnumerable<Position> positions)
        {
            _positions = positions;
        }

        public bool CanExecute(IGrid grid)
        {
            return true;
        }

        public void Execute(IGrid grid)
        {
            foreach( var pos in _positions )
            {
                grid.SetValue(pos, InputValue.Empty);

            }
        }
    }
}

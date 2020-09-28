using Core.Data;

namespace Application.Managers
{
    public class ToolManager
    {
        public void UseMarker(Grid grid, Position position, Value value)
        {
            if( grid.GetIsGiven(position) ) return;

            if( grid.GetValue(position) == Value.None )
            {
                grid.SetValue(position, value);
            }
            else if( grid.GetValue(position) == value )
            {
                grid.SetValue(position, Value.None);
            }
        }

        public void UsePencil(Grid grid, Position position, Value value)
        {
            if( grid.HasValue(position) ) return;

            grid.ToggleCandidate(position, value);
        }

        public void UseEraser(Grid grid, Position position)
        {
            if( grid.GetIsGiven(position) ) return;

            grid.SetValue(position, Value.None);
            grid.ClearCandidates(position);
        }
    }
}

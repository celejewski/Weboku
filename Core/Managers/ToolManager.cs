using Core.Data;

namespace Core.Managers
{
    public class ToolManager
    {
        public void UseMarker(IGrid grid, Position position, InputValue value)
        {
            if( grid.GetIsGiven(position) ) return;

            if( grid.GetValue(position) == InputValue.None )
            {
                grid.SetValue(position, value);
            }
            else if( grid.GetValue(position) == value )
            {
                grid.SetValue(position, InputValue.None);
            }
        }

        public void UsePencil(IGrid grid, Position position, InputValue value)
        {
            if( grid.HasValue(position) ) return;

            grid.ToggleCandidate(position, value);
        }

        public void UseEraser(IGrid grid, Position position)
        {
            if( grid.GetIsGiven(position) ) return;

            grid.SetValue(position, InputValue.None);
            grid.ClearCandidates(position);
        }
    }
}

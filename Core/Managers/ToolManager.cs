using Core.Data;

namespace Core.Managers
{
    public class ToolManager
    {
        public void UseMarker(IGrid grid, Position pos, InputValue value)
        {
            if( grid.GetIsGiven(pos) ) return;

            if( grid.GetValue(pos) == InputValue.None )
            {
                grid.SetValue(pos, value);
            }
            else if( grid.GetValue(pos) == value )
            {
                grid.SetValue(pos, InputValue.None);
            }
        }

        public void UsePencil(IGrid grid, Position pos, InputValue value)
        {
            if( grid.HasValue(pos) ) return;

            grid.ToggleCandidate(pos, value);
        }

        public void UseEraser(IGrid grid, Position pos)
        {
            if( grid.GetIsGiven(pos) ) return;

            grid.SetValue(pos, InputValue.None);
            grid.ClearCandidates(pos);
        }
    }
}

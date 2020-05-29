using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public CellColor Color1 { get; set; }
        public CellColor Color2 { get; set; }
        public MouseEventArgs MouseEventArgs { get; set; }
    }
}

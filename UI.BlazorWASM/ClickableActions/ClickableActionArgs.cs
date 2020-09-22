using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionArgs
    {
        public Position Position { get; set; }
        public Value Value { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public MouseEventArgs MouseEventArgs { get; set; }
    }
}

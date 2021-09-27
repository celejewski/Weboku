using Microsoft.AspNetCore.Components.Web;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.UserInterface.ClickableActions
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
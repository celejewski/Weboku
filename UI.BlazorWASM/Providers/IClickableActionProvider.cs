using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public interface IClickableActionProvider : IProvider
    {
        Value Value { get; set; }
        Color Color1 { get; set; }
        Color Color2 { get; set; }
        void SetClickableAction(IClickableAction clickableAction);
        IClickableAction ClickableAction { get; }

        void OnLeftClick(MouseEventArgs e, Position pos);
        void OnRightClick(MouseEventArgs e, Position pos);
    }
}

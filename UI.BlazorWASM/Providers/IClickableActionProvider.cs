using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public interface IClickableActionProvider : IProvider
    {
        int Value { get; set; }
        CellColor Color1 { get; set; }
        CellColor Color2 { get; set; }
        void SetClickableAction(IClickableAction clickableAction);
        IClickableAction ClickableAction { get; }

        void OnLeftClick(MouseEventArgs e, int x, int y);
        void OnRightClick(MouseEventArgs e, int x, int y);
    }
}

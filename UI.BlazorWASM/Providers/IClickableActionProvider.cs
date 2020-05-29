using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.ClickableActions;

namespace UI.BlazorWASM.Providers
{
    public interface IClickableActionProvider : IProvider
    {
        int Value { get; set; }
        void SetClickableAction(IClickableAction clickableAction);
        IClickableAction ClickableAction { get; }

        void OnLeftClick(MouseEventArgs e, int x, int y);
        void OnRightClick(MouseEventArgs e, int x, int y);
    }
}

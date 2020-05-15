using UI.BlazorWASM.ClickableActions;

namespace UI.BlazorWASM.Providers
{
    public interface IClickableActionProvider : IProvider
    {
        void SetClickableAction(IClickableAction clickableAction);
        IClickableAction ClickableAction { get; }
    }
}

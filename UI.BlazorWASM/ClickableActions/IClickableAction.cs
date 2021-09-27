namespace Weboku.UserInterface.ClickableActions
{
    public interface IClickableAction
    {
        void LeftClickAction(ClickableActionArgs args);
        void RightClickAction(ClickableActionArgs args);
    }
}
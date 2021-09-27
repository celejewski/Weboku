using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public interface INumpadMenuItem : ICommand
    {
        string Tooltip { get; }
        bool IsDimmed { get; }
        bool IsSelectable { get; }
    }
}
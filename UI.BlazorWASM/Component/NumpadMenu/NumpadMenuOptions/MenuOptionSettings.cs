using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class MenuOptionSettings
    {
        public bool IsSelectable { get; set; }

        public string Tooltip { get; set; }
        public string Label { get; set; }
        public SelectableMenuItemContainer SelectableMenuItemContainer { get; set; }
        public ICommand Command { get; set; }
    }
}
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class ClearColorsMenuItem : NumpadMenuLabel
    {
        public ClearColorsMenuItem(MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
        }

        public override bool IsSelectable => false;

        public string Label => "numpad-clear-colors__label";

        public override string Tooltip => "numpad-clear-colors__tooltip";
    }
}
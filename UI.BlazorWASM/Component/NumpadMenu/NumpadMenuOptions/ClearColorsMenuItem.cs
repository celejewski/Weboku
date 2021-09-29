using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class ClearColorsMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public ClearColorsMenuItem(ICommand command)
            : base(command)
        {
        }

        public override bool IsDimmed => false;

        public override bool IsSelectable => false;

        public string Label => "numpad-clear-colors__label";

        public override string Tooltip => "numpad-clear-colors__tooltip";
    }
}
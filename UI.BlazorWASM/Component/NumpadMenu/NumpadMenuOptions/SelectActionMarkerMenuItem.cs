using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionMarkerMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionMarkerMenuItem(NumpadMenuProvider numpadMenuProvider, ICommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {
        }

        public string Label => "fas fa-marker";
        public override string Tooltip => "select-action-marker__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
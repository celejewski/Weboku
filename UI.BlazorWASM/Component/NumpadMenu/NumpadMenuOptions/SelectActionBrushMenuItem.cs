using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionBrushMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionBrushMenuItem(NumpadMenuProvider numpadMenuProvider, SelectActionBrushCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {
        }

        public string Label => "fas fa-paint-roller";
        public override string Tooltip => "select-action-brush__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
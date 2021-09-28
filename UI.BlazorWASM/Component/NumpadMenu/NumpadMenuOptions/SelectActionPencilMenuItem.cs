using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionPencilMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionPencilMenuItem(NumpadMenuProvider numpadMenuProvider, ICommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {
        }

        public string Label => "fas fa-pencil-alt";
        public override string Tooltip => "select-action-pencil__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
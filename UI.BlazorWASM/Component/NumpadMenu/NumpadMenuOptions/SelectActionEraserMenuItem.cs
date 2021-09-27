using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionEraserMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionEraserMenuItem(NumpadMenuProvider numpadMenuProvider, SelectActionEraserCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {
        }

        public string Label => "fas fa-eraser";
        public override string Tooltip => "select-action-eraser__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
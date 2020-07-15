using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
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

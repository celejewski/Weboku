using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionPencilMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionPencilMenuItem(NumpadMenuProvider numpadMenuProvider, SelectEraserActionCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-pencil-alt";
        public override string Tooltip => "select-action-pencil__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

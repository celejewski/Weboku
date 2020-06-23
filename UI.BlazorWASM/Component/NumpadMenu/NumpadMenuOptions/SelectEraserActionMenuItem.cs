using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectEraserActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectEraserActionMenuItem(NumpadMenuProvider numpadMenuProvider, SelectEraserActionCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-pencil-alt";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

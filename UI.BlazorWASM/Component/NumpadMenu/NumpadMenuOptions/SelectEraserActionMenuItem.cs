using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectEraserActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectEraserActionMenuItem(NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            :base(commandProvider.SelectEraserAction(), numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-pencil-alt";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

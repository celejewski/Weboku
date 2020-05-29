using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectEraserActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectEraserActionMenuItem(NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            :base(numpadMenuProvider, commandProvider.SelectEraserAction())
        {

        }
        public string Label => "Eraser";

        public override bool IsDimmed => false;

        public override bool IsSelectable => false;
    }
}

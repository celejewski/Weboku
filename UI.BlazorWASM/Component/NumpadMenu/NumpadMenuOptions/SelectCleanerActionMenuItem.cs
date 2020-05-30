using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectCleanerActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectCleanerActionMenuItem(NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            : base(commandProvider.SelectCleaner(), numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-eraser";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

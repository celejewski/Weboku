using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectCleanerActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectCleanerActionMenuItem(NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider)
            : base(numpadMenuProvider, commandProvider.SelectCleaner())
        {

        }
        public string Label => "X";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

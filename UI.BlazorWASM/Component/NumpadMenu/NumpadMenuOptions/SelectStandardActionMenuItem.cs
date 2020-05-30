using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectStandardActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectStandardActionMenuItem(NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            :base(commandProvider.SelectStandardAction(), numpadMenuProvider.ActionContainer)
        {

        }

        public string Label => "Std";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

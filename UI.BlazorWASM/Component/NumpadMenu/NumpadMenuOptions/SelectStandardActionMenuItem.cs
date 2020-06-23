using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectStandardActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectStandardActionMenuItem(NumpadMenuProvider numpadMenuProvider, SelectStandardActionCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {

        }

        public string Label => "fas fa-marker";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

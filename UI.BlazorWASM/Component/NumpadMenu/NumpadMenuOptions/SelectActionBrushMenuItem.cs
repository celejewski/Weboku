using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionBrushMenuItem : BaseMenuOption, INumpadMenuLabel
    {

        public SelectActionBrushMenuItem(NumpadMenuProvider numpadMenuProvider, SelectActionBrushCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-paint-roller";
        public override string Tooltip => "select-action-brush__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

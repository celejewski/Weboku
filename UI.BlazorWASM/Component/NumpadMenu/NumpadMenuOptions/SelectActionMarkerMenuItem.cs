using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectActionMarkerMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public SelectActionMarkerMenuItem(NumpadMenuProvider numpadMenuProvider, SelectActionMarkerCommand command)
            : base(command, numpadMenuProvider.ActionContainer)
        {

        }

        public string Label => "fas fa-marker";
        public override string Tooltip => "select-action-marker__tooltip";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}

using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class ClearColorsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public ClearColorsNumpadMenuItem(ClearColorsCommand command)
            : base(command)
        {
        }
        public override bool IsDimmed => false;

        public override bool IsSelectable => false;

        public string Label => "numpad-clear-colors__label";
    }
}

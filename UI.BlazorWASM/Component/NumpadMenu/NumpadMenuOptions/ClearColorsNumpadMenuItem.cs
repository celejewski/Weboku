using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class ClearColorsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public ClearColorsNumpadMenuItem(ClearColorsCommand command)
            :base(command)
        {
        }
        public override bool IsDimmed => false;

        public override bool IsSelectable => false;

        public string Label => "Clear colors";
    }
}

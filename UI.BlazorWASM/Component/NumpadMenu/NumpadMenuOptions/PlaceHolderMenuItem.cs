using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PlaceHolderMenuItem : INumpadMenuLabel, ISelectColorMenuItem
    {
        public string Label => string.Empty;

        public bool IsDimmed => false;

        public bool IsSelectable => false;

        public Color Color1 => Color.None;

        public Color Color2 => Color.None;

        public string Tooltip => "";

        public Task Execute() => Task.CompletedTask;
    }
}

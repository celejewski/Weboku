using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public interface ISelectColorMenuItem : INumpadMenuItem
    {
        Color Color1 { get; }
        Color Color2 { get; }
    }
}

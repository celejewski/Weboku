using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public interface ISelectColorMenuItem : INumpadMenuItem
    {
        CellColor Color1 { get; }
        CellColor Color2 { get; }
    }
}

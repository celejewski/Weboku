using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public interface INumpadMenuItem : ICommand
    {
        string Tooltip { get; }
        bool IsDimmed { get; }
        bool IsSelectable { get; }
    }
}

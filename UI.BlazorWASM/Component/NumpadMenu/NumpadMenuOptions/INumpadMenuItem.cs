using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public interface INumpadMenuItem : ICommand
    {
        bool IsDimmed { get; }
        bool IsSelectable { get; }
    }
}

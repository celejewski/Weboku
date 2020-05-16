namespace UI.BlazorWASM.ViewModels
{
    public interface INumpadMenuItem : ICommand
    {
        bool IsDimmed { get; }
        bool IsSelectable { get; }
    }
}

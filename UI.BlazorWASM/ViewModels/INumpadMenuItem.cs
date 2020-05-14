namespace UI.BlazorWASM.ViewModels
{
    public interface INumpadMenuItem : IMenuItem
    {
        bool IsDimmed { get; }
        bool IsSelectable { get; }
    }
}

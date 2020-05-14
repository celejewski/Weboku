namespace UI.BlazorWASM.ViewModels
{
    public interface IMenuItem : ICommand
    {
        string Label { get; }
    }
}

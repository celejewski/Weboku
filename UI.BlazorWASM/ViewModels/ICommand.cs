using System.Threading.Tasks;

namespace UI.BlazorWASM.ViewModels
{
    public interface ICommand
    {
        Task Execute();
        bool CanExecute { get; }
    }
}

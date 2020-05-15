using System.Threading.Tasks;

namespace UI.BlazorWASM.ViewModels
{
    public interface ICommand
    {
        void Execute();
        bool CanExecute { get; }
    }
}

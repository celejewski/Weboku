using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public interface ICommand
    {
        Task Execute();
    }
}

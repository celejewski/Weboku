using System.Threading.Tasks;

namespace Weboku.UserInterface.Commands
{
    public interface ICommand
    {
        Task Execute();
    }
}
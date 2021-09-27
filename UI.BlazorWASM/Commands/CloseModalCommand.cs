using System.Threading.Tasks;
using Weboku.Application.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class CloseModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public CloseModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            return Task.CompletedTask;
        }
    }
}
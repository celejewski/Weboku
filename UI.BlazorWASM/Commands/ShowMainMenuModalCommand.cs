using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowMainMenuModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowMainMenuModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.MainMenu);
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowSettingsModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowSettingsModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.Settings);
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowHowToPlayModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowHowToPlayModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.HowToPlay);
            return Task.CompletedTask;
        }
    }
}
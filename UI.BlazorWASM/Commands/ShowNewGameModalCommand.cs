using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowNewGameModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowNewGameModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.NewGame);
            return Task.CompletedTask;
        }
    }
}
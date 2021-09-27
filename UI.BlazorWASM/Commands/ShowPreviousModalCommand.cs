using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowPreviousModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowPreviousModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            if (_modalProvider.HasPreviousState)
            {
                _modalProvider.GoToPreviousState();
            }

            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowPasteModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowPasteModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.Paste);
            return Task.CompletedTask;
        }
    }
}
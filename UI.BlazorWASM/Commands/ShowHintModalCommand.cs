using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowHintModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;
        private readonly HintsProvider _hintsProvider;

        public ShowHintModalCommand(ModalProvider modalProvider, HintsProvider hintsProvider)
        {
            _modalProvider = modalProvider;
            _hintsProvider = hintsProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.Hints);
            _hintsProvider.SetState(Hints.HintsState.ShowEmpty);
            return Task.CompletedTask;
        }
    }
}
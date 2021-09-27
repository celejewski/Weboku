using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class StartGameCommand
    {
        private readonly ModalProvider _modalProvider;
        private readonly DomainFacade _domainFacade;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartGameCommand(
            ModalProvider modalProvider,
            DomainFacade domainFacade,
            GameTimerProvider gameTimerProvider)
        {
            _modalProvider = modalProvider;
            _domainFacade = domainFacade;
            _gameTimerProvider = gameTimerProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            _domainFacade.ClearAllColors();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
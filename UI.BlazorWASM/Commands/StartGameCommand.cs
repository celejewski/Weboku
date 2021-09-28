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

        public StartGameCommand(
            ModalProvider modalProvider,
            DomainFacade domainFacade)
        {
            _modalProvider = modalProvider;
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            _domainFacade.ClearAllColors();
            _domainFacade.StartTimer();
            return Task.CompletedTask;
        }
    }
}
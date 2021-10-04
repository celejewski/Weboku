using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Commands
{
    public class StartGameCommand
    {
        private readonly DomainFacade _modalProvider;
        private readonly DomainFacade _domainFacade;

        public StartGameCommand(
            DomainFacade modalProvider,
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
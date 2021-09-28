using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public RestartGameCommand(
            DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.RestartGrid();
            _domainFacade.ClearAllColors();
            _domainFacade.StartTimer();
            return Task.CompletedTask;
        }
    }
}
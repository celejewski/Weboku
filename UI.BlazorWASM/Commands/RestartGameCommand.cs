using System.Threading.Tasks;
using Weboku.Application;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly GameTimerProvider _gameTimerProvider;

        public RestartGameCommand(
            DomainFacade domainFacade,
            GameTimerProvider gameTimerProvider)
        {
            _domainFacade = domainFacade;
            _gameTimerProvider = gameTimerProvider;
        }

        public Task Execute()
        {
            _domainFacade.RestartGrid();
            _domainFacade.ClearAllColors();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
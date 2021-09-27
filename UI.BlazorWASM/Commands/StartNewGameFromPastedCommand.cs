using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public StartNewGameFromPastedCommand(
            DomainFacade domainFacade,
            StartGameCommand startGameCommand)
        {
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }

        public async Task Execute()
        {
            if (_domainFacade.PastedIsValid)
            {
                _domainFacade.StartNewGameFromPasted();
                await _startGameCommand.Execute();
            }
        }
    }
}
using Application;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
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
            _domainFacade.StartNewGameFromPasted();
            await _startGameCommand.Execute();
        }
    }
}

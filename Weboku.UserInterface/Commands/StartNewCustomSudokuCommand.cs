using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class StartNewCustomSudokuCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public StartNewCustomSudokuCommand(DomainFacade domainFacade, StartGameCommand startGameCommand)
        {
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }

        public async Task Execute()
        {
            if (_domainFacade.IsCustomGridValid)
            {
                _domainFacade.StartNewCustomGame();
                await _startGameCommand.Execute();
            }
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
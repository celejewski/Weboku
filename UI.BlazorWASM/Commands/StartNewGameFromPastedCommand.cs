using Core;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly PasteProvider _pasteProvider;
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public StartNewGameFromPastedCommand(
            PasteProvider pasteProvider,
            DomainFacade domainFacade,
            StartGameCommand startGameCommand)
        {
            _pasteProvider = pasteProvider;
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }

        public async Task Execute()
        {
            var grid = _pasteProvider.Grid.Clone();
            _domainFacade.StartNewGame(grid);
            _pasteProvider.RestartBackup();
            await _startGameCommand.Execute();
        }
    }
}

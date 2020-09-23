using Core;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly PasteProvider _pasteProvider;
        private readonly StartGameCommand _startGameCommand;

        public StartNewGameFromPastedCommand(
            DomainFacade domainFacade,
            PasteProvider pasteProvider,
            StartGameCommand startGameCommand)
        {
            _domainFacade = domainFacade;
            _pasteProvider = pasteProvider;
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

using Weboku.Application;
using Weboku.Core.Data;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Providers
{
    public class CommandProvider
    {
        private readonly SettingsProvider _settingsProvider;
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public CommandProvider(
            SettingsProvider settingsProvider,
            DomainFacade domainFacade,
            StartGameCommand startGameCommand
        )
        {
            _settingsProvider = settingsProvider;
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }

        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _domainFacade);
        }

        public ICommand StartNewGameV2(Difficulty difficulty)
        {
            return new StartNewGameCommand(difficulty, _domainFacade, _startGameCommand);
        }

        public ICommand SetLanguage(string name)
        {
            return new SetLanguageCommand(name, _settingsProvider);
        }
    }
}
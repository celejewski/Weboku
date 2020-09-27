using Application;
using Core.Data;
using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly ClickableActionProvider _clickableActionProvider;
        private readonly SettingsProvider _settingsProvider;
        private readonly DomainFacade _domainFacade;
        private readonly StartGameCommand _startGameCommand;

        public CommandProvider(
            ClickableActionProvider clickableActionProvider,
            SettingsProvider settingsProvider,
            DomainFacade domainFacade,
            StartGameCommand startGameCommand

            )
        {
            _clickableActionProvider = clickableActionProvider;
            _settingsProvider = settingsProvider;
            _domainFacade = domainFacade;
            _startGameCommand = startGameCommand;
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _domainFacade, _clickableActionProvider);
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

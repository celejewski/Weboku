using System;
using Weboku.Application;
using Weboku.Application.Filters;
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
            void execute()
            {
                _domainFacade.SelectValue(value);
                var selectedValueFilter = new SelectedValueFilter(value);
                _domainFacade.SetFilter(selectedValueFilter);
            }

            return new RelayCommand
            (
                execute,
                () => _domainFacade.CanUseValueFilter(value)
            );
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
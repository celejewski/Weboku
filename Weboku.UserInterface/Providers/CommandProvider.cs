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

        public CommandProvider(
            SettingsProvider settingsProvider,
            DomainFacade domainFacade
        )
        {
            _settingsProvider = settingsProvider;
            _domainFacade = domainFacade;
        }

        public ICommand SelectValue(int value)
        {
            void execute()
            {
                _domainFacade.SelectValue(value);
            }

            return new RelayCommand
            (
                execute,
                () => _domainFacade.CanUseValueFilter(value)
            );
        }


        public ICommand SetLanguage(string name)
        {
            return new SetLanguageCommand(name, _settingsProvider);
        }
    }
}
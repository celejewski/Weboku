using Weboku.Application;
using Weboku.UserInterface.Commands;

namespace Weboku.UserInterface.Providers
{
    public class CommandProvider
    {
        private readonly DomainFacade _domainFacade;

        public CommandProvider(DomainFacade domainFacade)
        {
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
    }
}
using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Filters;

namespace Weboku.UserInterface.Commands
{
    public class SelectValueCommand : ICommand
    {
        private readonly int _value;
        private readonly DomainFacade _domainFacade;

        public SelectValueCommand(int value, DomainFacade domainFacade)
        {
            _value = value;
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.SetFilter(new SelectedValueFilter(_value));
            _domainFacade.SelectValue(_value);
            return Task.CompletedTask;
        }
    }
}
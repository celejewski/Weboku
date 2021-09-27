using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class RedoCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public RedoCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.Redo();
            return Task.CompletedTask;
        }
    }
}
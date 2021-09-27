using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class UndoCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public UndoCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.Undo();
            return Task.CompletedTask;
        }
    }
}
using Application;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
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

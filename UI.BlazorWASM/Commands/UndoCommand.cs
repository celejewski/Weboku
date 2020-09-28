using Application;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
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

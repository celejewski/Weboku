using Core;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class RedoCommand : ICommand
    {
        private readonly DomainFacade _gridHistoryManager;

        public RedoCommand(DomainFacade gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }

        public Task Execute()
        {
            _gridHistoryManager.Redo();
            return Task.CompletedTask;
        }
    }
}

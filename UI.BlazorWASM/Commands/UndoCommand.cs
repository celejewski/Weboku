using Core;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class UndoCommand : ICommand
    {
        private readonly DomainFacade _gridHistoryManager;

        public UndoCommand(DomainFacade gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public Task Execute()
        {
            _gridHistoryManager.Undo();
            return Task.CompletedTask;
        }
    }
}

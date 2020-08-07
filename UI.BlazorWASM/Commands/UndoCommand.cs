using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class UndoCommand : ICommand
    {
        private readonly GridHistoryProvider _gridHistoryManager;

        public UndoCommand(GridHistoryProvider gridHistoryManager)
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

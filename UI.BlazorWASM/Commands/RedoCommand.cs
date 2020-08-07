using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RedoCommand : ICommand
    {
        private readonly GridHistoryProvider _gridHistoryManager;

        public RedoCommand(GridHistoryProvider gridHistoryManager)
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

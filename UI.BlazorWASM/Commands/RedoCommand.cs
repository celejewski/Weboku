using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Commands
{
    public class RedoCommand : ICommand
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public RedoCommand(IGridHistoryManager gridHistoryManager)
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

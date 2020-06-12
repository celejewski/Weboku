using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ClearCandidatesCommand : ICommand
    {
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        public ClearCandidatesCommand(IGridProvider gridProvider, IGridHistoryManager gridHistoryManager)
        {
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.ClearCandidates();
            return Task.CompletedTask;
        }
    }
}

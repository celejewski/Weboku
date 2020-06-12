using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGridProvider _gridProvider;

        public FindAllCandidatesCommand(IGridHistoryManager gridHistoryManager, IGridProvider gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _gridProvider = gridProvider;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.FindAllCandidates();
            return Task.CompletedTask;
        }
    }
}

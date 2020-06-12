using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public FindAllCandidatesCommand(IGridHistoryManager gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
#warning todo
            return Task.CompletedTask;
        }
    }
}

using Core;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly DomainFacade _gridProvider;

        public FindAllCandidatesCommand(GridHistoryProvider gridHistoryManager, DomainFacade gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _gridProvider = gridProvider;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.FillAllLegalCandidates();
            return Task.CompletedTask;
        }
    }
}

using Core;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly DomainFacade _gridProvider;

        public FindAllCandidatesCommand(DomainFacade gridProvider)
        {
            _gridProvider = gridProvider;
        }
        public Task Execute()
        {
            _gridProvider.FillAllLegalCandidates();
            return Task.CompletedTask;
        }
    }
}

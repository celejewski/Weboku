using Core;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public FindAllCandidatesCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }
        public Task Execute()
        {
            _domainFacade.FillAllLegalCandidates();
            return Task.CompletedTask;
        }
    }
}

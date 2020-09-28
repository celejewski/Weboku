using Application;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class ClearCandidatesCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public ClearCandidatesCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.ClearAllCandidates();
            return Task.CompletedTask;
        }
    }
}

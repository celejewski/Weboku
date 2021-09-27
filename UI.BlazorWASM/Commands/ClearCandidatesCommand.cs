using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
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
using System.Threading.Tasks;
using Application;

namespace Weboku.UserInterface.Commands
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
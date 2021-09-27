using System.Threading.Tasks;
using Weboku.Application;

namespace Weboku.UserInterface.Commands
{
    public class ClearColorsCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public ClearColorsCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.ClearAllColors();
            return Task.CompletedTask;
        }
    }
}
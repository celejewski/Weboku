using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Commands
{
    public class SelectActionMarkerCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public SelectActionMarkerCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.SelectTool(Tool.Marker);
            return Task.CompletedTask;
        }
    }
}
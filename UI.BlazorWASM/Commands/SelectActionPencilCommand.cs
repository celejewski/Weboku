using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Commands
{
    public class SelectActionPencilCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public SelectActionPencilCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.SelectTool(Tool.Pencil);
            return Task.CompletedTask;
        }
    }
}
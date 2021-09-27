using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Commands
{
    public class SelectActionBrushCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;

        public SelectActionBrushCommand(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public Task Execute()
        {
            _domainFacade.SelectTool(Tool.Brush);
            return Task.CompletedTask;
        }
    }
}
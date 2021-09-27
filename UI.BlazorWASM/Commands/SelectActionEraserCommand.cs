using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Application.Filters;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class SelectActionEraserCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectActionEraserCommand(
            DomainFacade filterProvider,
            NumpadMenuProvider numpadMenuProvider)
        {
            _domainFacade = filterProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public Task Execute()
        {
            _domainFacade.SelectTool(Tool.Eraser);
            _domainFacade.SetFilter(new EraseFilter());
            _numpadMenuProvider.FilterContainer.DeselectItem();
            return Task.CompletedTask;
        }
    }
}
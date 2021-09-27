using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Filters;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class SelectActionEraserCommand : ICommand
    {
        private readonly ClickableActionProvider _clickableActionProvider;
        private readonly DomainFacade _domainFacade;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectActionEraserCommand(
            ClickableActionProvider clickableActionProvider,
            DomainFacade filterProvider,
            NumpadMenuProvider numpadMenuProvider)
        {
            _clickableActionProvider = clickableActionProvider;
            _domainFacade = filterProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public Task Execute()
        {
            _clickableActionProvider.SelectClickableAction(ClickableAction.Eraser);
            _domainFacade.SetFilter(new EraseFilter());
            _numpadMenuProvider.FilterContainer.DeselectItem();
            return Task.CompletedTask;
        }
    }
}
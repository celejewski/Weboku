using Application;
using Application.Filters;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
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

using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectCleanerAction : ICommand
    {
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;
        private readonly FilterProvider _filterProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectCleanerAction(
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            FilterProvider filterProvider,
            NumpadMenuProvider numpadMenuProvider)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
            _filterProvider = filterProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.EraserAction());
            _filterProvider.SetFilter(new EraseFilter());
            _numpadMenuProvider.FilterContainer.DeselectItem();
            return Task.CompletedTask;
        }
    }
}

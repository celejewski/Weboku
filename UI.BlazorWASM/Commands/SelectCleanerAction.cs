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
        private readonly IFilterProvider _filterProvider;

        public SelectCleanerAction(
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            IFilterProvider filterProvider)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
            _filterProvider = filterProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.CleanerAction());
            _filterProvider.SetFilter(new EraseFilter());
            return Task.CompletedTask;
        }
    }
}

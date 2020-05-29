using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectValueCommand : ICommand
    {
        private readonly int _value;
        private readonly IFilterProvider _filterProvider;
        private readonly ClickableActionFactory _clickableActionFactory;
        private readonly IClickableActionProvider _clickableActionProvider;

        public SelectValueCommand(int value, IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider, ClickableActionFactory clickableActionFactory)
        {
            _value = value;
            _filterProvider = filterProvider;
            _clickableActionFactory = clickableActionFactory;
            _clickableActionProvider = clickableActionProvider;
        }

        public Task Execute()
        {
            _filterProvider.SetFilter(new SelectedValueFilter(_value));
            _clickableActionProvider.Value = _value;
            return Task.CompletedTask;

        }
    }
}

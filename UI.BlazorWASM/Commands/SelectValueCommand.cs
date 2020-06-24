using Core.Data;
using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectValueCommand : ICommand
    {
        private readonly int _value;
        private readonly FilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public SelectValueCommand(int value, FilterProvider filterProvider, IClickableActionProvider clickableActionProvider)
        {
            _value = value;
            _filterProvider = filterProvider;
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

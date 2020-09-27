using Application;
using Application.Filters;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectValueCommand : ICommand
    {
        private readonly int _value;
        private readonly DomainFacade _domainFacade;
        private readonly ClickableActionProvider _clickableActionProvider;

        public SelectValueCommand(int value, DomainFacade filterProvider, ClickableActionProvider clickableActionProvider)
        {
            _value = value;
            _domainFacade = filterProvider;
            _clickableActionProvider = clickableActionProvider;
        }

        public Task Execute()
        {
            _domainFacade.SetFilter(new SelectedValueFilter(_value));
            _clickableActionProvider.Value = _value;
            return Task.CompletedTask;

        }
    }
}

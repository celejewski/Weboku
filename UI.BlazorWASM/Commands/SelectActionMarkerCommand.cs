using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionMarkerCommand : ICommand
    {
        private readonly ClickableActionProvider _clickableActionProvider;

        public SelectActionMarkerCommand(ClickableActionProvider clickableActionProvider)
        {
            _clickableActionProvider = clickableActionProvider;
        }

        public Task Execute()
        {
            _clickableActionProvider.SelectClickableAction(ClickableAction.Marker);
            return Task.CompletedTask;
        }
    }
}

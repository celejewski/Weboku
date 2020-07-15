using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionMarkerCommand : ICommand
    {
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;

        public SelectActionMarkerCommand(IClickableActionProvider clickableActionProvider, ClickableActionFactory clickableActionFactory)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
        }

        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.MarkerAction());
            return Task.CompletedTask;
        }
    }
}

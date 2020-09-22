using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionMarkerCommand : ICommand
    {
        private readonly ClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;

        public SelectActionMarkerCommand(ClickableActionProvider clickableActionProvider, ClickableActionFactory clickableActionFactory)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
        }

        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.MakeMarkerAction());
            return Task.CompletedTask;
        }
    }
}

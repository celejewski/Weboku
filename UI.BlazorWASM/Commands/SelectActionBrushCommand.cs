using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionBrushCommand : ICommand
    {
        private readonly ClickableActionProvider _clickableActionProvider;

        public SelectActionBrushCommand(ClickableActionProvider clickableActionProvider)
        {
            _clickableActionProvider = clickableActionProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.SelectClickableAction(ClickableAction.Brush);
            return Task.CompletedTask;
        }
    }
}

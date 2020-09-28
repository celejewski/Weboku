using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionPencilCommand : ICommand
    {
        private readonly ClickableActionProvider _clickableActionProvider;
        public SelectActionPencilCommand(ClickableActionProvider clickableActionProvider)
        {
            _clickableActionProvider = clickableActionProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.SelectClickableAction(ClickableAction.Pencil);
            return Task.CompletedTask;
        }
    }
}

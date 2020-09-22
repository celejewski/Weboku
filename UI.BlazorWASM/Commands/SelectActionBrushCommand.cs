using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectActionBrushCommand : ICommand
    {
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;

        public SelectActionBrushCommand(IClickableActionProvider clickableActionProvider, ClickableActionFactory clickableActionFactory)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
        }
        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.MakeBrushAction());
            return Task.CompletedTask;
        }
    }
}

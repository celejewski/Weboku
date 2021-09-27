using System.Threading.Tasks;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
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
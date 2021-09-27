using System.Threading.Tasks;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
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
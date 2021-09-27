using System.Threading.Tasks;
using Weboku.Application.Enums;
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
            _clickableActionProvider.SelectClickableAction(Tool.Marker);
            return Task.CompletedTask;
        }
    }
}
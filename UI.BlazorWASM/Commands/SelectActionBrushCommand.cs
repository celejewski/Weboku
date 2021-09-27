using System.Threading.Tasks;
using Weboku.Application.Enums;
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
            _clickableActionProvider.SelectClickableAction(Tool.Brush);
            return Task.CompletedTask;
        }
    }
}
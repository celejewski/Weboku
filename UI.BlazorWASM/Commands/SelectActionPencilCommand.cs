using System.Threading.Tasks;
using Weboku.Application.Enums;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
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
            _clickableActionProvider.SelectClickableAction(Tool.Pencil);
            return Task.CompletedTask;
        }
    }
}
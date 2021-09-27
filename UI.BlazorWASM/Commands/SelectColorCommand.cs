using System.Threading.Tasks;
using Weboku.UserInterface.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class SelectColorCommand : ICommand
    {
        private readonly Color _color;
        private readonly ClickableActionProvider _clickableActionProvider;

        public SelectColorCommand(Color color, ClickableActionProvider clickableActionProvider)
        {
            _color = color;
            _clickableActionProvider = clickableActionProvider;
        }

        public Task Execute()
        {
            _clickableActionProvider.Color1 = _color;
            _clickableActionProvider.Color2 = _color;
            return Task.CompletedTask;
        }
    }
}
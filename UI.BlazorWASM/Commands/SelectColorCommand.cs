using System.Threading.Tasks;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectColorCommand : ICommand
    {
        private readonly Color _cellColor;
        private readonly ClickableActionProvider _clickableActionProvider;

        public SelectColorCommand(Color cellColor, ClickableActionProvider clickableActionProvider)
        {
            _cellColor = cellColor;
            _clickableActionProvider = clickableActionProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.Color1 = _cellColor;
            _clickableActionProvider.Color2 = _cellColor;
            return Task.CompletedTask;
        }
    }
}

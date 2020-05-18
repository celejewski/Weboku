using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Converters;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class SelectColorMenuItem : ISelectColorMenuItem
    {
        private readonly CellColor _cellColor;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public SelectColorMenuItem(CellColor cellColor, ICellColorProvider cellColorProvider, IClickableActionProvider clickableActionProvider)
        {
            _cellColor = cellColor;
            _cellColorProvider = cellColorProvider;
            _clickableActionProvider = clickableActionProvider;
        }

        public string CssClass => CellColorConverter.ToCssClass(_cellColor);

        public bool IsDimmed => false;

        public bool IsSelectable => true;

        public bool CanExecute => true;

        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(new ColorAction(_cellColor, _cellColorProvider));
            return Task.CompletedTask;
        }
    }
}

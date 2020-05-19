using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectValue : ICommand
    {
        private readonly int _value;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public SelectValue(int value, IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider, IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider)
        {
            _value = value;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
        }

        public Task Execute()
        {
            _filterProvider.SetFilter(new SelectedValueFilter(_value));
            _clickableActionProvider.SetClickableAction(new StandardAction(_gridHistoryManager, _cellColorProvider, _sudokuProvider, _value));
            return Task.CompletedTask;

        }
    }
}

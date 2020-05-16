using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuBuilder
    {
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public NumpadMenuBuilder(IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider, IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider)
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }

        public SelectValueNumpadMenuItem SelectValue(int value)
        {
            return new SelectValueNumpadMenuItem(value, _filterProvider, _clickableActionProvider, _gridHistoryManager, _cellColorProvider, _sudokuProvider);
        }

        public RedoNumpadMenuItem Redo()
        {
            return new RedoNumpadMenuItem(_gridHistoryManager);
        }

        public UndoNumpadMenuItem Undo()
        {
            return new UndoNumpadMenuItem(_gridHistoryManager);
        }

        public PairsNumpadMenuItem Pairs()
        {
            return new PairsNumpadMenuItem(_filterProvider);
        }

        public ClearColorsNumpadMenuItem ClearColors()
        {
            return new ClearColorsNumpadMenuItem(_cellColorProvider);
        }

        public SelectColorMenuItem SelectColor(CellColor cellColor)
        {
            return new SelectColorMenuItem(cellColor, _cellColorProvider, _clickableActionProvider);
        }

        public PlaceHolderNumpadMenuItem PlaceHolder()
        {
            return new PlaceHolderNumpadMenuItem();
        }
    }
}

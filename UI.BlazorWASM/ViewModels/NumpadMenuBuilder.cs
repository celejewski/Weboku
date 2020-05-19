using Core.Data;
using System.Collections.Generic;
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
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public NumpadMenuBuilder(
            IFilterProvider filterProvider, 
            IClickableActionProvider clickableActionProvider, 
            IGridHistoryManager gridHistoryManager, 
            ICellColorProvider cellColorProvider, 
            ISudokuProvider sudokuProvider, 
            NumpadMenuProvider numpadMenuProvider
            )
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public SelectValueNumpadMenuItem SelectValue(int value)
        {
            var command = new SelectValueNumpadMenuItem(value, _filterProvider, _clickableActionProvider, _gridHistoryManager, _cellColorProvider, _sudokuProvider, _numpadMenuProvider);
            return command;
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_gridHistoryManager);
            return command;
        }

        public UndoNumpadMenuItem Undo()
        {
            var command = new UndoNumpadMenuItem(_gridHistoryManager);
            return command;
        }

        public PairsNumpadMenuItem Pairs()
        {
            var command = new PairsNumpadMenuItem(_filterProvider, _sudokuProvider, _numpadMenuProvider);
            return command;
        }

        public ClearColorsNumpadMenuItem ClearColors()
        {
            var command = new ClearColorsNumpadMenuItem(_cellColorProvider);
            return command;
        }

        public SelectColorMenuItem SelectColor(CellColor cellColor)
        {
            var command = new SelectColorMenuItem(cellColor, _cellColorProvider, _clickableActionProvider);
            return command;
        }

        public PlaceHolderNumpadMenuItem PlaceHolder()
        {
            return new PlaceHolderNumpadMenuItem();
        }
    }
}

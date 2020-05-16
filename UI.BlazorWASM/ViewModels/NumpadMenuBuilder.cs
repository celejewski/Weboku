﻿using UI.BlazorWASM.Enums;
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
        private readonly IHotkeyProvider _hotkeyProvider;

        public NumpadMenuBuilder(
            IFilterProvider filterProvider, 
            IClickableActionProvider clickableActionProvider, 
            IGridHistoryManager gridHistoryManager, 
            ICellColorProvider cellColorProvider, 
            ISudokuProvider sudokuProvider, 
            NumpadMenuProvider numpadMenuProvider,
            HotkeyProvider hotkeyProvider
            )
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _numpadMenuProvider = numpadMenuProvider;
            _hotkeyProvider = hotkeyProvider;
        }

        public SelectValueNumpadMenuItem SelectValue(int value)
        {
            var command = new SelectValueNumpadMenuItem(value, _filterProvider, _clickableActionProvider, _gridHistoryManager, _cellColorProvider, _sudokuProvider, _numpadMenuProvider);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = value.ToString() });
            return command;
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "y" });
            return command;
        }

        public UndoNumpadMenuItem Undo()
        {
            var command = new UndoNumpadMenuItem(_gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "z" });
            return command;
        }

        public PairsNumpadMenuItem Pairs()
        {
            return new PairsNumpadMenuItem(_filterProvider, _sudokuProvider, _numpadMenuProvider);
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

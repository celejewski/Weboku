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
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "y", Ctrl = true });
            return command;
        }

        public UndoNumpadMenuItem Undo()
        {
            var command = new UndoNumpadMenuItem(_gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "z", Ctrl = true });
            return command;
        }

        public PairsNumpadMenuItem Pairs()
        {
            var command = new PairsNumpadMenuItem(_filterProvider, _sudokuProvider, _numpadMenuProvider);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "x" });
            return command;
        }

        public ClearColorsNumpadMenuItem ClearColors()
        {
            var command = new ClearColorsNumpadMenuItem(_cellColorProvider);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "h" });
            return command;
        }

        public SelectColorMenuItem SelectColor(CellColor cellColor)
        {
            var dict = new Dictionary<CellColor, string>
            {
                { CellColor.First, "a" },
                { CellColor.Second, "s" },
                { CellColor.Third, "d" },
                { CellColor.Fourth, "f" }
            };

            var command = new SelectColorMenuItem(cellColor, _cellColorProvider, _clickableActionProvider);
            if (dict.ContainsKey(cellColor) )
            {
                _hotkeyProvider.Register(new Hotkey { Command = command, Key = dict[cellColor] });
            }
            return command;
        }

        public PlaceHolderNumpadMenuItem PlaceHolder()
        {
            return new PlaceHolderNumpadMenuItem();
        }
    }
}
